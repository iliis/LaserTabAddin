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

        /*
         * Modes:
         * - fixed number of tabs
         * - tab size should be >=, <= or as close as possible to $x
         * - force parity (even/odd)
         * 
         * depth:
         * - same as thickness
         * - custom: [input field]
         * 
         * - flip/invert
         * 
         */

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
            //m_selects.SingleSelectEnabled = true;
            m_selects.MouseMoveEnabled = false; // recommended for performance if not used
            m_selects.Enabled = true; // ???
            // no idea what these event sinks are and why we need them...
            m_selects.OnSelect += new Inventor.SelectEventsSink_OnSelectEventHandler(this.SelectEvents_OnSelect);
            m_interaction.OnTerminate += new Inventor.InteractionEventsSink_OnTerminateEventHandler(this.M_interaction_OnTerminate);
            

            m_dialog = new LaserTabForm();

            m_dialog.setLabel("select a face");
            m_dialog.Show(new InventorMainFrame(m_inventorApplication.MainFrameHWND));
            m_dialog.FormClosed += M_dialog_FormClosed;
            m_dialog.button_ok.Click += Button_ok_Click;

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

        }


        private void Button_ok_Click(object sender, EventArgs e)
        {
            // references to some useful objects
            TransientGeometry geom = m_inventorApplication.TransientGeometry;
            PartDocument document = m_inventorApplication.ActiveDocument as PartDocument;
            PartComponentDefinition def = document.ComponentDefinition;
            UserParameters user_params = def.Parameters.UserParameters;
            UnitsOfMeasure units = document.UnitsOfMeasure;

            // get and check selected faces
            ObjectsEnumerator JustSelectedEntities = m_selects.SelectedEntities;

            if (JustSelectedEntities.Count == 0)
            {
                m_inventorApplication.ErrorManager.Show("Select at least one planar, rectangular face.", true, false);
                return;
            }

            foreach (Object _f in JustSelectedEntities)
            {
                Face f = _f as Face;
                if (f == null)
                {
                    m_inventorApplication.ErrorManager.Show("Somehow, you managed to select something that isn't a face. This should not happen, please report it.", true, false);
                    return;
                }

                if (f.Edges.Count != 4)
                {
                    m_inventorApplication.ErrorManager.Show("Please only select rectangular faces.", true, false);
                    return;
                }
            }

                // TODO: catch exception when invalid was is entered
                UserParameter tab_user_constr;
            if (m_dialog.mode_count.Checked)
            {
                tab_user_constr = user_params.AddByExpression("tab_count", m_dialog.tab_size_input.Text, UnitsTypeEnum.kUnitlessUnits);
            }
            else
            {
                tab_user_constr = user_params.AddByExpression("tab_size", m_dialog.tab_size_input.Text, UnitsTypeEnum.kDefaultDisplayLengthUnits);
            }
            


            int total_operations = JustSelectedEntities.Count;

            Edge[] long_edge = new Edge[total_operations];
            bool[] long_edge_dir = new bool[total_operations];
            PlanarSketch[] all_sketches = new PlanarSketch[total_operations];
            Profile[] profile = new Profile[total_operations];
            ExtrudeFeature[] extrusion = new ExtrudeFeature[total_operations];
            TwoPointDistanceDimConstraint[] tab_length_constr = new TwoPointDistanceDimConstraint[total_operations];
            TwoPointDistanceDimConstraint[] tab_widthdepth_constr = new TwoPointDistanceDimConstraint[total_operations];
            TwoPointDistanceDimConstraint[] total_length_constr = new TwoPointDistanceDimConstraint[total_operations];
            RectangularPatternFeature[] rect_pattern = new RectangularPatternFeature[total_operations];

            Transaction transaction = m_inventorApplication.TransactionManager.StartTransaction(m_inventorApplication.ActiveDocument, "LaserTab");

            // create extrusion feature for each face
            int i = 0;
            foreach (Object _f in JustSelectedEntities)
            {
                Face f = _f as Face;

                // TODO: make sure active document is a partDocument and ActiveEditObject is not a sketch (should be also a partDocument?)
                // TODO: wrap it all into a ClientFeature
                // TODO: maybe also wrap it in a Transaction?


                if (getEdgeLength(f.Edges[1]) > getEdgeLength(f.Edges[2]))
                {
                    long_edge[i] = f.Edges[1];
                }
                else
                {
                    long_edge[i] = f.Edges[2];
                }

                // create sketch

                PlanarSketch sketch = def.Sketches.Add(f, true); // project existing geometry
                                                                 //PlanarSketch sketch = def.Sketches.AddWithOrientation(f, long_edge, true, true, long_edge.StartVertex, true);

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

                // remember wheter 'long_edge' starts or stops at 'P_orig' (which is important for the direction of the rectangular pattern)
                Vertices vert_tmp = P_orig.ReferencedEntity as Vertices;
                Debug.Assert(vert_tmp != null);
                Debug.Assert(vert_tmp.Count == 1);
                long_edge_dir[i] = long_edge[i].StartVertex == vert_tmp[1];



                // appearantly, Profiles.AddForSolid() doesn't like lines that are made entirely out of projected points... (maybe because the line already exists?)
                SketchPoint P_orig_proj = P_orig, P_short_proj = P_short;

                P_short = sketch.SketchPoints.Add(P_short_proj.Geometry, false);
                P_orig = sketch.SketchPoints.Add(P_orig_proj.Geometry, false);

                sketch.GeometricConstraints.AddCoincident((SketchEntity)P_short, (SketchEntity)P_short_proj);
                sketch.GeometricConstraints.AddCoincident((SketchEntity)P_orig, (SketchEntity)P_orig_proj);

                // create dimension constraints

                // TODO: calculate better position for text label

                // driven constraint of short dimension (determining thickness and depth of tab)
                tab_widthdepth_constr[i] = sketch.DimensionConstraints.AddTwoPointDistance(
                    P_orig, P_short, DimensionOrientationEnum.kAlignedDim,
                    P_short.Geometry, true);

                // driven constraint of long dimenstion (determining number/size of tabs)
                total_length_constr[i] = sketch.DimensionConstraints.AddTwoPointDistance(
                    P_orig, P_long, DimensionOrientationEnum.kAlignedDim,
                    P_long.Geometry, true);

                //Debug.Print("constraint short: {0} = {1}", constr_short.Parameter.Expression, constr_short.Parameter.Value);
                //Debug.Print("constraint long: {0} = {1}", constr_long.Parameter.Expression, constr_long.Parameter.Value);

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
                sketch.GeometricConstraints.AddCoincident((SketchEntity)long_line, (SketchEntity)P_end1_sk);

                // constraint for tab length (twice, once for each side of the rectangle)
                TwoPointDistanceDimConstraint tab_len_constraint1 = sketch.DimensionConstraints.AddTwoPointDistance(P_orig, P_end1_sk, DimensionOrientationEnum.kAlignedDim, P_end1);
                TwoPointDistanceDimConstraint tab_len_constraint2 = sketch.DimensionConstraints.AddTwoPointDistance(P_short, P_end2_sk, DimensionOrientationEnum.kAlignedDim, P_end2);
                tab_length_constr[i] = tab_len_constraint1;

                // {0}: total length
                // {1}: user input (count or length of single tab)
                string expr;
                if (m_dialog.mode_count.Checked)
                {
                    expr = "{0} / {1}";
                }
                else
                {
                    // TODO: take dropdown of >/</~ into account
                    expr = "{0} / round({0}/{1})";
                }

                tab_len_constraint1.Parameter.Expression = string.Format(expr, total_length_constr[i].Parameter.Name, tab_user_constr.Name);
                tab_len_constraint2.Parameter.Expression = tab_len_constraint1.Parameter.Name;

                // create a rectangle based on these points
                // two-point rectangle is always axis-aligned -> doesn't work for rotated stuff
                //SketchEntitiesEnumerator rect = sketch.SketchLines.AddAsTwoPointRectangle(P_orig, P_end_sk);
                // this is cumbersome, as the third point is transient and therefore the rectangle would have to be constrained afterwards
                //SketchEntitiesEnumerator rect = sketch.SketchLines.AddAsThreePointRectangle(P_orig, P_short, P_end);


                ObjectCollection rect = m_inventorApplication.TransientObjects.CreateObjectCollection();
                rect.Add(sketch.SketchLines.AddByTwoPoints(P_orig, P_end1_sk));
                rect.Add(sketch.SketchLines.AddByTwoPoints(P_end1_sk, P_end2_sk));
                rect.Add(sketch.SketchLines.AddByTwoPoints(P_end2_sk, P_short));
                rect.Add(sketch.SketchLines.AddByTwoPoints(P_short, P_orig));
                
                profile[i] = sketch.Profiles.AddForSolid(false, rect);
                all_sketches[i] = sketch;

                i++;
            }

            // do extrusions
            for (i = 0; i < total_operations; i++)
            {
                // extrude said rectangle
                ExtrudeDefinition extrusion_def = document.ComponentDefinition.Features.ExtrudeFeatures.CreateExtrudeDefinition(profile[i], PartFeatureOperationEnum.kCutOperation);
                extrusion_def.SetDistanceExtent(1, PartFeatureExtentDirectionEnum.kNegativeExtentDirection);
                extrusion[i] = document.ComponentDefinition.Features.ExtrudeFeatures.Add(extrusion_def);
            }

            // do rectangular patterns
            for (i = 0; i < total_operations; i++)
            {
                // now repeat that extrusion
                ObjectCollection col = m_inventorApplication.TransientObjects.CreateObjectCollection();
                col.Add(extrusion[i]);

                // TODO: is ceil() actually correct here?
                string count_expr = string.Format("ceil({0} / {1} / 2)", total_length_constr[i].Parameter.Name, tab_length_constr[i].Parameter.Name);

                RectangularPatternFeatureDefinition pattern_def =
                document.ComponentDefinition.Features.RectangularPatternFeatures.CreateDefinition(
                    col, long_edge[i], long_edge_dir[i], count_expr, tab_length_constr[i].Parameter.Name + "*2");
                // TODO: we could use PatternSpacingType kFitToPathLength here...

                rect_pattern[i] =
                document.ComponentDefinition.Features.RectangularPatternFeatures.AddByDefinition(pattern_def);
            }
            transaction.End();

            stop_selection();


            ClientFeatureDefinition feature_def = def.Features.ClientFeatures.CreateDefinition("LaserTab", all_sketches[0], rect_pattern[total_operations - 1]);
            def.Features.ClientFeatures.Add(feature_def, "0defbf22-e302-4266-9bc9-fb80d5c8eb7e");
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
