using System;
using System.Runtime.InteropServices;
using Inventor;
using Microsoft.Win32;
using stdole;
using System.Windows.Forms;
using System.Resources;
using System.Collections.Generic;
using System.Diagnostics;

namespace LaserTabAddin
{
    /// <summary>
    /// This is the primary AddIn Server class that implements the ApplicationAddInServer interface
    /// that all Inventor AddIns are required to implement. The communication between Inventor and
    /// the AddIn is via the methods on this interface.
    /// </summary>
    [GuidAttribute("0defbf22-e302-4266-9bc9-fb80d5c8eb7e")]
    public class StandardAddInServer : Inventor.ApplicationAddInServer
    {

        // Inventor application object.
        private Inventor.Application m_inventorApplication;

        private ButtonDefinition m_buttonDef;
        private UserInterfaceEvents m_uiEvents;
        private InteractionEvents m_interaction;
        private SelectEvents m_selects; // keeping a reference to the events objects seems to be crucial! (even tough it is part of m_interaction)
        private LaserTabForm m_dialog;

        public StandardAddInServer()
        {
        }
        
        #region ApplicationAddInServer Members

        public void Activate(Inventor.ApplicationAddInSite addInSiteObject, bool firstTime)
        {
            // This method is called by Inventor when it loads the addin.
            // The AddInSiteObject provides access to the Inventor Application object.
            // The FirstTime flag indicates if the addin is loaded for the first time.

            // Initialize AddIn members.
            m_inventorApplication = addInSiteObject.Application;

            // TODO: Add ApplicationAddInServer.Activate implementation.
            // e.g. event initialization, command creation etc.

            // Get a reference to the UserInterfaceManager object.
            Inventor.UserInterfaceManager UIManager = m_inventorApplication.UserInterfaceManager;

            // Get a reference to the ControlDefinitions object.
            ControlDefinitions controlDefs = m_inventorApplication.CommandManager.ControlDefinitions;

            // Get the images from the resources.  They are stored as .Net images and the
            // PictureConverter class is used to convert them to IPictureDisp objects, which
            // the Inventor API requires. 
            stdole.IPictureDisp icon_large = PictureConverter.ImageToPictureDisp(Properties.Resources.icon32);
            stdole.IPictureDisp icon_small = PictureConverter.ImageToPictureDisp(Properties.Resources.icon16);

            // Create the button definition.
            m_buttonDef = controlDefs.AddButtonDefinition("make tabs", "UIRibbonSampleOne",
                                                 CommandTypesEnum.kNonShapeEditCmdType,
                                                 "0defbf22-e302-4266-9bc9-fb80d5c8eb7e", "", "", icon_small, icon_large);

            // Call the function to add information to the user-interface.
            if (firstTime)
            {
                CreateUserInterface();
                //PrintRibbonNames();
            }

            // Connect to UI events to be able to handle a UI reset.
            m_uiEvents = m_inventorApplication.UserInterfaceManager.UserInterfaceEvents;
            m_uiEvents.OnResetRibbonInterface += m_uiEvents_OnResetRibbonInterface;

            m_buttonDef.OnExecute += m_buttonDef_OnExecute;
        }

        private void CreateUserInterface()
        {
            // Get a reference to the UserInterfaceManager object. 
            Inventor.UserInterfaceManager UIManager = m_inventorApplication.UserInterfaceManager;

            // Get the zero doc ribbon.
            Inventor.Ribbon zeroRibbon = UIManager.Ribbons["Part"];

            // Get the getting started tab.
            Inventor.RibbonTab startedTab = zeroRibbon.RibbonTabs["id_TabModel"];

            // Get the new features panel.
            Inventor.RibbonPanel newFeaturesPanel = startedTab.RibbonPanels["id_PanelP_ModelModify"];

            // Add a button to the panel, using the previously created button definition.
            newFeaturesPanel.CommandControls.AddButton(m_buttonDef, true);
        }

        private void m_uiEvents_OnResetRibbonInterface(NameValueMap context)
        {
            CreateUserInterface();
        }

        private void m_buttonDef_OnExecute(NameValueMap context)
        {
            if (m_dialog != null)
            {
                m_dialog.Focus();
                return;
            }

            m_interaction = m_inventorApplication.CommandManager.CreateInteractionEvents();
            m_interaction.StatusBarText = "Select the face to add tabs to";
            m_selects = m_interaction.SelectEvents;
            m_selects.ClearSelectionFilter();
            m_selects.AddSelectionFilter(SelectionFilterEnum.kPartFacePlanarFilter);
            m_selects.SingleSelectEnabled = true;
            m_selects.MouseMoveEnabled = false; // recommended for performance if not used
            m_selects.Enabled = true; // ???
            // no idea what these event sinks are and why we need them...
            m_selects.OnSelect += new Inventor.SelectEventsSink_OnSelectEventHandler(this.SelectEvents_OnSelect);
            m_interaction.OnTerminate += new Inventor.InteractionEventsSink_OnTerminateEventHandler(this.M_interaction_OnTerminate);
            

            m_dialog = new LaserTabForm();

            m_dialog.setLabel("select a face");
            m_dialog.Show(new InventorMainFrame(m_inventorApplication.MainFrameHWND));
            m_dialog.FormClosed += M_dialog_FormClosed;

            m_interaction.Start();

        }

        private void M_interaction_OnTerminate()
        {
            stop_selection();
        }

        private void M_dialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            stop_selection();
        }

        private void stop_selection()
        {
            m_interaction.SelectEvents.OnSelect -= new Inventor.SelectEventsSink_OnSelectEventHandler(this.SelectEvents_OnSelect);
            m_interaction.OnTerminate -= new Inventor.InteractionEventsSink_OnTerminateEventHandler(this.M_interaction_OnTerminate);
            m_interaction.Stop();
            m_interaction = null;

            m_dialog.FormClosed -= M_dialog_FormClosed;
            m_dialog.Close();
            m_dialog = null;
        }

        private double getEdgeLength(Edge e)
        {
            double min_parm, max_parm, length;
            e.Evaluator.GetParamExtents(out min_parm, out max_parm);
            e.Evaluator.GetLengthAtParam(min_parm, max_parm, out length);
            return length;
        }

        private void SelectEvents_OnSelect(ObjectsEnumerator JustSelectedEntities, SelectionDeviceEnum SelectionDevice, Point ModelPosition, Point2d ViewPosition, Inventor.View View)
        {
            if (JustSelectedEntities.Count != 1)
            {
                // TODO: use proper error popup (use ErrorManager)
                m_dialog.setLabel(string.Format("Wut? You selected {} objects.", JustSelectedEntities.Count));
                return;
            }
            
            if (JustSelectedEntities[1] is Face)
            {
                Face f = JustSelectedEntities[1] as Face;
                m_dialog.setLabel(string.Format("selected: {0}, area = {1}, edges = {2}", f.InternalName, f.Evaluator.Area, f.Edges.Count));


                // TODO: check that f.Edges.Count == 4 and that all of them have geometry type kLineSegmentCurve
                // TODO: make sure active document is a partDocument and ActiveEditObject is not a sketch (should be also a partDocument?)
                // TODO: wrap it all into a ClientFeature
                // TODO: maybe also wrap it in a Transaction?

                
                Edge short_edge, long_edge;
                
                if (getEdgeLength(f.Edges[1]) > getEdgeLength(f.Edges[2]))
                {
                    short_edge = f.Edges[2];
                    long_edge  = f.Edges[1];
                }
                else
                {
                    short_edge = f.Edges[1];
                    long_edge  = f.Edges[2];
                }

                m_dialog.setEdgeInfo(getEdgeLength(short_edge).ToString(), getEdgeLength(long_edge).ToString());
                

                // create sketch
                TransientGeometry geom = m_inventorApplication.TransientGeometry;
                PartDocument document = m_inventorApplication.ActiveDocument as PartDocument;
                PartComponentDefinition def = document.ComponentDefinition;
                PlanarSketch sketch = def.Sketches.Add(f, true); // project existing geometry

                //PlanarSketch sketch = def.Sketches.AddWithOrientation(f, long_edge, true, true, long_edge.StartVertex, true);

                foreach (SketchPoint point in sketch.SketchPoints)
                {
                    Debug.Print("point at X: {0}, Y: {1}", point.Geometry.X, point.Geometry.Y);
                }

                foreach (SketchLine line in sketch.SketchLines)
                {
                    Debug.Print("line from {0}, {1} to {2}, {3}, length = {4}", line.StartSketchPoint.Geometry.X, line.StartSketchPoint.Geometry.Y, line.EndSketchPoint.Geometry.X, line.EndSketchPoint.Geometry.Y, line.Length);
                    //SketchConstraintsEnumerator constr = line.Constraints[0];
                    foreach (object constr in line.Constraints)
                    {
                        Debug.Print(" > with constraint: {0}, type = {1}", constr, constr.GetType());
                    }
                }


                // figure out orientation
                SketchLine short_line, long_line, long_line2;
                if (sketch.SketchLines[1].Length < sketch.SketchLines[2].Length)
                {
                    short_line = sketch.SketchLines[1];
                    long_line = sketch.SketchLines[2];
                    long_line2 = sketch.SketchLines[4]; // opposite of other long line
                }
                else
                {
                    short_line = sketch.SketchLines[2];
                    long_line = sketch.SketchLines[1];
                    long_line2 = sketch.SketchLines[3];
                }

                SketchPoint P_orig, P_long, P_short;
                if (short_line.EndSketchPoint == long_line.StartSketchPoint)
                {
                    P_orig = short_line.EndSketchPoint;
                    P_short = short_line.StartSketchPoint;
                    P_long = long_line.EndSketchPoint;
                }
                else if (short_line.StartSketchPoint == long_line.EndSketchPoint)
                {
                    P_orig = short_line.StartSketchPoint;
                    P_short = short_line.EndSketchPoint;
                    P_long = long_line.StartSketchPoint;
                }
                else if (short_line.StartSketchPoint == long_line.StartSketchPoint)
                {
                    P_orig = short_line.StartSketchPoint;
                    P_short = short_line.EndSketchPoint;
                    P_long = long_line.EndSketchPoint;
                }
                else
                {
                    Debug.Assert(short_line.EndSketchPoint == long_line.EndSketchPoint);

                    P_orig = short_line.EndSketchPoint;
                    P_short = short_line.StartSketchPoint;
                    P_long = long_line.StartSketchPoint;
                }


                // appearantly, Profiles.AddForSolid() doesn't like lines that are made entirely out of projected points... (maybe because the line already exists?)
                SketchPoint P_orig_proj = P_orig, P_short_proj = P_short;

                P_short = sketch.SketchPoints.Add(P_short_proj.Geometry, false);
                P_orig  = sketch.SketchPoints.Add(P_orig_proj.Geometry, false);

                sketch.GeometricConstraints.AddCoincident((SketchEntity) P_short, (SketchEntity) P_short_proj);
                sketch.GeometricConstraints.AddCoincident((SketchEntity)P_orig, (SketchEntity)P_orig_proj);

                // create dimension constraints

                // TODO: calculate better position for text label
                TwoPointDistanceDimConstraint constr_short = sketch.DimensionConstraints.AddTwoPointDistance(
                    P_orig, P_short, DimensionOrientationEnum.kAlignedDim,
                    P_short.Geometry, true);

                TwoPointDistanceDimConstraint constr_long = sketch.DimensionConstraints.AddTwoPointDistance(
                    P_orig, P_long, DimensionOrientationEnum.kAlignedDim,
                    P_long.Geometry, true);
                
                Debug.Print("constraint short: {0} = {1}", constr_short.Parameter.Expression, constr_short.Parameter.Value);
                Debug.Print("constraint long: {0} = {1}", constr_long.Parameter.Expression, constr_long.Parameter.Value);

                // create endpoint for rectangle
                Point2d P_end2 = P_short.Geometry.Copy();
                Point2d P_end1 = P_orig.Geometry.Copy();

                Vector2d long_direction = P_orig.Geometry.VectorTo(P_long.Geometry);
                long_direction.ScaleBy(0.2);

                P_end1.TranslateBy(long_direction);
                P_end2.TranslateBy(long_direction);

                SketchPoint P_end1_sk = sketch.SketchPoints.Add(P_end1, false);
                SketchPoint P_end2_sk = sketch.SketchPoints.Add(P_end2, false);

                // constrain endpoints properly
                sketch.GeometricConstraints.AddCoincident((SketchEntity)long_line2, (SketchEntity)P_end2_sk);
                sketch.GeometricConstraints.AddCoincident((SketchEntity)long_line,  (SketchEntity)P_end1_sk);

                TwoPointDistanceDimConstraint tab_len_constraint1 = sketch.DimensionConstraints.AddTwoPointDistance(P_orig,  P_end1_sk, DimensionOrientationEnum.kAlignedDim, P_end1);
                TwoPointDistanceDimConstraint tab_len_constraint2 = sketch.DimensionConstraints.AddTwoPointDistance(P_short, P_end2_sk, DimensionOrientationEnum.kAlignedDim, P_end2);
                
                tab_len_constraint1.Parameter.Expression = string.Format("{0} / 10", constr_long.Parameter.Name);
                tab_len_constraint2.Parameter.Expression = tab_len_constraint1.Parameter.Name;

                // create a rectangle based on these points
                // two-point rectangle is always axis-aligned -> doesn't work for rotated stuff
                //SketchEntitiesEnumerator rect = sketch.SketchLines.AddAsTwoPointRectangle(P_orig, P_end_sk);
                // this is cumbersome, as the third point is transient and therefore the rectangle would have to be constrained afterwards
                //SketchEntitiesEnumerator rect = sketch.SketchLines.AddAsThreePointRectangle(P_orig, P_short, P_end);

                
                SketchLine l1 = sketch.SketchLines.AddByTwoPoints(P_orig, P_end1_sk);
                SketchLine l2 = sketch.SketchLines.AddByTwoPoints(P_end1_sk, P_end2_sk);
                SketchLine l3 = sketch.SketchLines.AddByTwoPoints(P_end2_sk, P_short);
                SketchLine l4 = sketch.SketchLines.AddByTwoPoints(P_short, P_orig);
                ObjectCollection rect = m_inventorApplication.TransientObjects.CreateObjectCollection();
                rect.Add(l1);
                rect.Add(l2);
                rect.Add(l3);
                rect.Add(l4);

                Debug.Assert(l1.EndSketchPoint == l2.StartSketchPoint);
                Debug.Assert(l2.EndSketchPoint == l3.StartSketchPoint);
                Debug.Assert(l3.EndSketchPoint == l4.StartSketchPoint);
                Debug.Assert(l4.EndSketchPoint == l1.StartSketchPoint);

                Debug.Print("got {0} objects in collection", rect.Count);
                foreach (Object o in rect)
                {
                    Debug.Print("object: {0}", o);
                    SketchLine line = o as SketchLine;
                    Debug.Print("  -> line from {0}, {1} to {2}, {3}, length = {4}", line.StartSketchPoint.Geometry.X, line.StartSketchPoint.Geometry.Y, line.EndSketchPoint.Geometry.X, line.EndSketchPoint.Geometry.Y, line.Length);
                }



                Profile profile = sketch.Profiles.AddForSolid(false, rect);

                // extrude said rectangle
                ExtrudeDefinition extrusion_def = document.ComponentDefinition.Features.ExtrudeFeatures.CreateExtrudeDefinition(profile, PartFeatureOperationEnum.kCutOperation);
                extrusion_def.SetDistanceExtent(1, PartFeatureExtentDirectionEnum.kNegativeExtentDirection);
                ExtrudeFeature extrusion = document.ComponentDefinition.Features.ExtrudeFeatures.Add(extrusion_def);

                // now repeat that extrusion
                ObjectCollection col = m_inventorApplication.TransientObjects.CreateObjectCollection();
                col.Add(extrusion);
                // TODO: figure out correct direction for pattern here
                RectangularPatternFeatureDefinition pattern_def =
                document.ComponentDefinition.Features.RectangularPatternFeatures.CreateDefinition(
                    col, long_edge, false, 10/2, tab_len_constraint1.Parameter.Name + "*2");
                // TODO: we could use PatternSpacingType kFitToPathLength here...

                RectangularPatternFeature rect_pattern =
                document.ComponentDefinition.Features.RectangularPatternFeatures.AddByDefinition(pattern_def);

                /*
                // create lines
                SketchLines lines = sketch.SketchLines;
                lines.AddByTwoPoints(sketch.SketchPoints[1], sketch.SketchPoints[2]);
                lines.AddByTwoPoints(sketch.SketchPoints[2], sketch.SketchPoints[3]);
                lines.AddByTwoPoints(sketch.SketchPoints[3], sketch.SketchPoints[1]);

                // create profile
                Profile profile = sketch.Profiles.AddForSolid();

                // create extrusion
                ExtrudeDefinition extrusion_def = document.ComponentDefinition.Features.ExtrudeFeatures.CreateExtrudeDefinition(profile, PartFeatureOperationEnum.kCutOperation);
                extrusion_def.SetDistanceExtent(1, PartFeatureExtentDirectionEnum.kNegativeExtentDirection);
                ExtrudeFeature extrusion = document.ComponentDefinition.Features.ExtrudeFeatures.Add(extrusion_def);
                */

                /*
                Debug.Print("selected a face, area: {0}", f.Evaluator.Area);
                foreach (Edge e in f.Edges)
                {
                    Debug.Print("got edge: geometry type = {0}", e.GeometryType);
                    
                    LineSegment seg = e.Geometry as LineSegment;
                    double min_parm, max_parm;
                    e.Evaluator.GetParamExtents(out min_parm, out max_parm);
                    Debug.Print("    min parm: {0}, max parm: {1}", min_parm, max_parm);

                    double edge_len;
                    e.Evaluator.GetLengthAtParam(min_parm, max_parm, out edge_len);

                    Debug.Print("    length: {0} cm", edge_len)
                    
                }*/
            }
            else
            {
                m_dialog.setLabel("Please select a face. This should not happen!");
            }
        }

        public void Deactivate()
        {
            // This method is called by Inventor when the AddIn is unloaded.
            // The AddIn will be unloaded either manually by the user or
            // when the Inventor session is terminated

            // TODO: Add ApplicationAddInServer.Deactivate implementation

            // Release objects.
            m_inventorApplication = null;

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public void ExecuteCommand(int commandID)
        {
            // Note:this method is now obsolete, you should use the 
            // ControlDefinition functionality for implementing commands.
        }

        public object Automation
        {
            // This property is provided to allow the AddIn to expose an API 
            // of its own to other programs. Typically, this  would be done by
            // implementing the AddIn's API interface in a class and returning 
            // that class object through this property.

            get
            {
                // TODO: Add ApplicationAddInServer.Automation getter implementation
                return null;
            }
        }

        #endregion


        private void PrintRibbonNames()
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\Samuel Bryner\ribbons.txt"))
            {
                Inventor.UserInterfaceManager UIManager = m_inventorApplication.UserInterfaceManager;

                // Get the zero doc ribbon.
                foreach (Ribbon rib in UIManager.Ribbons)
                {
                    file.WriteLine(rib.InternalName);
                    foreach (RibbonTab tab in rib.RibbonTabs)
                    {
                        file.WriteLine(string.Format("  - Tab: internal name = '{0}', display name = '{1}'", tab.InternalName, tab.DisplayName));
                        foreach (RibbonPanel panel in tab.RibbonPanels)
                        {
                            file.WriteLine(string.Format("      - Panel: {0} = '{1}'", panel.DisplayName, panel.InternalName));
                        }
                    }
                }
            }
        }
    }
}
