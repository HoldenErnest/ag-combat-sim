//Code for Chat/ChatInterface (Container)
using AdvCore.UI.Components.Controls;
using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;
using GumRuntime;
using MonoGameGum;
using MonoGameGum.GueDeriving;
using RenderingLibrary.Graphics;
using System.Linq;
namespace AdvCore.UI.Components.Chat;
partial class ChatInterface : global::Gum.Forms.Controls.FrameworkElement
{
    [System.Runtime.CompilerServices.ModuleInitializer]
    public static void RegisterRuntimeType()
    {
        var template = new global::Gum.Forms.VisualTemplate((vm, createForms) =>
        {
            var visual = new global::MonoGameGum.GueDeriving.ContainerRuntime();
            var element = ObjectFinder.Self.GetElementSave("Chat/ChatInterface") ?? throw new System.InvalidOperationException("Could not find an element named Chat/ChatInterface - did you forget to load a Gum project?");
            element.SetGraphicalUiElement(visual, RenderingLibrary.SystemManagers.Default);
            if(createForms) visual.FormsControlAsObject = new ChatInterface(visual);
            return visual;
        });
        global::Gum.Forms.Controls.FrameworkElement.DefaultFormsTemplates[typeof(ChatInterface)] = template;
        ElementSaveExtensions.RegisterGueInstantiation("Chat/ChatInterface", () => 
        {
            var gue = template.CreateContent(null, true) as InteractiveGue;
            return gue;
        });
    }
    public enum FocusCatagory
    {
        Unfocused,
        Focused,
    }

    FocusCatagory? _focusCatagoryState;
    public FocusCatagory? FocusCatagoryState
    {
        get => _focusCatagoryState;
        set
        {
            _focusCatagoryState = value;
            if(value != null)
            {
                if(Visual.Categories.ContainsKey("FocusCatagory"))
                {
                    var category = Visual.Categories["FocusCatagory"];
                    var state = category.States.Find(item => item.Name == value.ToString());
                    this.Visual.ApplyState(state);
                }
                else
                {
                    var category = ((global::Gum.DataTypes.ElementSave)this.Visual.Tag).Categories.FirstOrDefault(item => item.Name == "FocusCatagory");
                    var state = category.States.Find(item => item.Name == value.ToString());
                    this.Visual.ApplyState(state);
                }
            }
        }
    }
    public TextBox TextEditor { get; protected set; }
    public ListBox ListBox { get; protected set; }

    public ChatInterface(InteractiveGue visual) : base(visual)
    {
    }
    public ChatInterface()
    {



    }
    protected override void ReactToVisualChanged()
    {
        base.ReactToVisualChanged();
        TextEditor = global::Gum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<TextBox>(this.Visual,"TextEditor");
        ListBox = global::Gum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<ListBox>(this.Visual,"ListBox");
        CustomInitialize();
    }
    //Not assigning variables because Object Instantiation Type is set to By Name rather than Fully In Code
    partial void CustomInitialize();
}
