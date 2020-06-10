using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Microsoft.VisualStudio.CommandBars;
using Extensibility;
using EnvDTE;
using EnvDTE80;
using System.ComponentModel;

public class Connect : IDTExtensibility2, IDTCommandTarget {
    private DTE2 _applicationObject;
    private AddIn _addInInstance;
    private CodeGeneratorForm myForm;

    /// <summary>Implements the constructor for the Add-in object. Place your initialization code within this method.</summary>
    public Connect() {
    }
    // Private Sub Handle_ProjectAdded()
    // Dim listOfMasterPages As BindingList(Of MasterPageClass) = StaticVariables.MasterPages
    // End Sub
    // Private Sub Handle_ProjectRemoved()
    // StaticVariables.MasterPages = New BindingList(Of MasterPageClass)
    // End Sub
    private void Handle_ProjectOpened() {
        BindingList<MasterPageClass> listOfMasterPages = StaticVariables.Instance.MasterPages;
        StaticVariables.Instance.IsProjectOpen = true;

        HandleProjectState();
    }
    private void Handle_ProjectClosed() {
        StaticVariables.Instance.MasterPages.Clear();
        StaticVariables.Instance.IsProjectOpen = false;
        HandleProjectState();
    }
    private static SolutionEvents _events;
    /// <summary>Implements the OnConnection method of the IDTExtensibility2 interface. Receives notification that the Add-in is being loaded.</summary>
    ///     '''<param name='application'>Root object of the host application.</param>
    ///     '''<param name='connectMode'>Describes how the Add-in is being loaded.</param>
    ///     '''<param name='addInInst'>Object representing this Add-in.</param>
    ///     '''<remarks></remarks>
    public void OnConnection(object application, ext_ConnectMode connectMode, object addInInst, ref Array custom) {
        _applicationObject = (DTE2)application;
        _events = _applicationObject.Events.SolutionEvents;
        _addInInstance = (AddIn)addInInst;
        if (_applicationObject != null && _applicationObject.Solution != null) {
            if (_applicationObject.Solution.Projects.Count > 0) {
                Project myProject = _applicationObject.Solution.Projects.Item(1);
                if (myProject != null)
                    StaticVariables.Instance.IsProjectOpen = true;
            }
        }
        if (connectMode == ext_ConnectMode.ext_cm_UISetup) {
            Commands2 commands = (Commands2)_applicationObject.Commands;
            string toolsMenuName;
            try {

                // If you would like to move the command to a different menu, change the word "Tools" to the 
                // English version of the menu. This code will take the culture, append on the name of the menu
                // then add the command to that menu. You can find a list of all the top-level menus in the file
                // CommandBar.resx.
                System.Resources.ResourceManager resourceManager = new System.Resources.ResourceManager("CommandBar", System.Reflection.Assembly.GetExecutingAssembly());

                System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo(_applicationObject.LocaleID);
                if ((cultureInfo.TwoLetterISOLanguageName == "zh")) {
                    System.Globalization.CultureInfo parentCultureInfo = cultureInfo.Parent;
                    toolsMenuName = resourceManager.GetString(string.Concat(parentCultureInfo.Name, "Tools"));
                } else
                    toolsMenuName = resourceManager.GetString(string.Concat(cultureInfo.TwoLetterISOLanguageName, "Tools"));
            } catch (Exception e) {
                // We tried to find a localized version of the word Tools, but one was not found.
                // Default to the en-US word, which may work for the current culture.
                toolsMenuName = "Tools";
            }

            // Place the command on the tools menu.
            // Find the MenuBar command bar, which is the top-level command bar holding all the main menu items:
            CommandBars commandBars = (CommandBars)_applicationObject.CommandBars;
            Microsoft.VisualStudio.CommandBars.CommandBar menuBarCommandBar = commandBars.Item("MenuBar");

            // Find the Tools command bar on the MenuBar command bar:
            CommandBarControl toolsControl = menuBarCommandBar.Controls.Item(toolsMenuName);
            CommandBarPopup toolsPopup = (CommandBarPopup)toolsControl;

            try {
                // Add a command to the Commands collection:
                Command command = commands.AddNamedCommand2(_addInInstance, "CodeGeneratorAddIn", "CodeGeneratorAddIn", "Executes the command for CodeGeneratorAddIn", true, 59, null/* TODO Change to default(_) if this is not a reference type */, System.Convert.ToInt32(vsCommandStatus.vsCommandStatusSupported) + System.Convert.ToInt32(vsCommandStatus.vsCommandStatusEnabled), vsCommandStyle.vsCommandStylePictAndText, vsCommandControlType.vsCommandControlTypeButton);

                // Find the appropriate command bar on the MenuBar command bar:
                command.AddControl(toolsPopup.CommandBar, 1);
            } catch (ArgumentException argumentException) {
            }
        }
    }

    /// <summary>Implements the OnDisconnection method of the IDTExtensibility2 interface. Receives notification that the Add-in is being unloaded.</summary>
    ///     '''<param name='disconnectMode'>Describes how the Add-in is being unloaded.</param>
    ///     '''<param name='custom'>Array of parameters that are host application specific.</param>
    ///     '''<remarks></remarks>
    public void OnDisconnection(ext_DisconnectMode disconnectMode, ref Array custom) {
    }

    /// <summary>Implements the OnAddInsUpdate method of the IDTExtensibility2 interface. Receives notification that the collection of Add-ins has changed.</summary>
    ///     '''<param name='custom'>Array of parameters that are host application specific.</param>
    ///     '''<remarks></remarks>
    public void OnAddInsUpdate(ref Array custom) {
    }

    /// <summary>Implements the OnStartupComplete method of the IDTExtensibility2 interface. Receives notification that the host application has completed loading.</summary>
    ///     '''<param name='custom'>Array of parameters that are host application specific.</param>
    ///     '''<remarks></remarks>
    public void OnStartupComplete(ref Array custom) {
    }

    /// <summary>Implements the OnBeginShutdown method of the IDTExtensibility2 interface. Receives notification that the host application is being unloaded.</summary>
    ///     '''<param name='custom'>Array of parameters that are host application specific.</param>
    ///     '''<remarks></remarks>
    public void OnBeginShutdown(ref Array custom) {
    }

    /// <summary>Implements the QueryStatus method of the IDTCommandTarget interface. This is called when the command's availability is updated</summary>
    ///     '''<param name='commandName'>The name of the command to determine state for.</param>
    ///     '''<param name='neededText'>Text that is needed for the command.</param>
    ///     '''<param name='status'>The state of the command in the user interface.</param>
    ///     '''<param name='commandText'>Text requested by the neededText parameter.</param>
    ///     '''<remarks></remarks>
    public void QueryStatus(string commandName, vsCommandStatusTextWanted neededText, ref vsCommandStatus status, ref object commandText) {
        if (neededText == vsCommandStatusTextWanted.vsCommandStatusTextWantedNone) {
            if (commandName == "Connect.CodeGeneratorAddIn")
                status = (vsCommandStatus)vsCommandStatus.vsCommandStatusEnabled + vsCommandStatus.vsCommandStatusSupported;
            else
                status = vsCommandStatus.vsCommandStatusUnsupported;
        }
    }

    /// <summary>Implements the Exec method of the IDTCommandTarget interface. This is called when the command is invoked.</summary>
    ///     '''<param name='commandName'>The name of the command to execute.</param>
    ///     '''<param name='executeOption'>Describes how the command should be run.</param>
    ///     '''<param name='varIn'>Parameters passed from the caller to the command handler.</param>
    ///     '''<param name='varOut'>Parameters passed from the command handler to the caller.</param>
    ///     '''<param name='handled'>Informs the caller if the command was handled or not.</param>
    ///     '''<remarks></remarks>
    public void Exec(string commandName, vsCommandExecOption executeOption, ref object varIn, ref object varOut, ref bool handled) {
        _events.Opened += Handle_ProjectOpened;
        _events.AfterClosing += Handle_ProjectClosed;
        handled = false;
        if (executeOption == vsCommandExecOption.vsCommandExecOptionDoDefault) {
            if (commandName == "Connect.CodeGeneratorAddIn") {
                myForm = new CodeGeneratorForm(_applicationObject);
                myForm.Show();
                HandleProjectState();
                handled = true;
                return;
            }
        }
    }
    private void HandleProjectState() {
        myForm.miCreateAndAddFiles.Enabled = StaticVariables.Instance.IsProjectOpen;
        myForm.miInsertCode.Enabled = StaticVariables.Instance.IsProjectOpen;
    }
}
