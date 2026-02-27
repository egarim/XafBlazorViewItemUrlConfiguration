# XAF Blazor ViewItem URL Configuration

**Working example of XAF ViewItems accessible via URL - copied from production XafGitHubCopilot**

This repository demonstrates how to make XAF Blazor ViewItems accessible through:
1. Traditional XAF navigation (standard)
2. Direct URL parameters (for deep linking, QR codes, bookmarks)
3. Programmatic access (for mobile apps, external systems)

## ✅ This is WORKING Code

This project is a **direct copy** of the working XafGitHubCopilot implementation with renamed namespaces.

**Status:** ✅ Builds successfully  
**Framework:** .NET 9.0  
**XAF Version:** Latest DevExpress packages  

## 🎯 What This Demonstrates

### The ViewItem Pattern

**CopilotChatViewItemBlazor.cs** - XAF ViewItem wrapper:
```csharp
public class CopilotChatViewItemBlazor : ViewItem, IComponentContentHolder
{
    RenderFragment IComponentContentHolder.ComponentContent => builder =>
    {
        builder.OpenComponent<CopilotChat>(0);
        // Configure component
        builder.CloseComponent();
    };
}
```

**CopilotChat.razor** - Reusable Blazor component:
```razor
@using DevExpress.Blazor.AIIntegration
<DxAIChat @ref="chat" ... />
```

### URL Access Pattern (To Be Added)

The pattern for URL-accessible pages:

```razor
@page "/chat"
@page "/chat/{InitialMessage?}"

<CopilotChat InitialMessage="@InitialMessage" />

@code {
    [Parameter]
    [SupplyParameterFromQuery(Name = "message")]
    public string? InitialMessage { get; set; }
}
```

## 🚀 Quick Start

### Prerequisites
- .NET 9.0 SDK
- DevExpress Universal subscription (v25.2+)
- DevExpress license activated on your machine

### Build

```bash
git clone https://github.com/egarim/XafBlazorViewItemUrlConfiguration.git
cd XafBlazorViewItemUrlConfiguration
dotnet build XafBlazorViewItemUrlConfiguration/XafBlazorViewItemUrlConfiguration.Blazor.Server/XafBlazorViewItemUrlConfiguration.Blazor.Server.csproj
```

### Run

```bash
cd XafBlazorViewItemUrlConfiguration/XafBlazorViewItemUrlConfiguration.Blazor.Server
dotnet run
```

Browse to: `https://localhost:5001`

## 📁 Project Structure

```
XafBlazorViewItemUrlConfiguration/
├── XafBlazorViewItemUrlConfiguration.Blazor.Server/
│   ├── Editors/CopilotChatViewItem/
│   │   ├── CopilotChat.razor              # Reusable component
│   │   └── CopilotChatViewItemBlazor.cs   # XAF ViewItem wrapper
│   └── Pages/
│       └── _Host.cshtml                    # Entry point
│
├── XafBlazorViewItemUrlConfiguration.Module/
│   ├── BusinessObjects/                    # Demo data model
│   ├── Services/                           # GitHub Copilot SDK integration
│   └── Controllers/                        # XAF controllers
│
└── XafBlazorViewItemUrlConfiguration.Win/  # Windows Forms version (optional)
```

## 🔧 Key Files

**Component:**
- `Editors/CopilotChatViewItem/CopilotChat.razor` - The reusable Blazor component

**ViewItem Wrapper:**
- `Editors/CopilotChatViewItem/CopilotChatViewItemBlazor.cs` - XAF integration

**Services:**
- `Services/CopilotChatService.cs` - GitHub Copilot SDK client
- `Services/CopilotChatClient.cs` - Chat client implementation
- `Services/CopilotToolsProvider.cs` - Tool definitions for AI

**Controllers:**
- `Controllers/ShowCopilotChatController.cs` - Action to show chat ViewItem
- `Controllers/SelectCopilotModelController.cs` - Model selection UI

## 🎨 Adapting This Pattern

### 1. Extract Your ViewItem Component

Take any XAF ViewItem and extract its UI to a standalone Blazor component.

### 2. Add URL Page (Next Step)

Create a standalone page that uses your component with URL parameters.

### 3. Add API Endpoint (Optional)

Create a REST API controller for programmatic access.

## 📚 Related Resources

**Original Project:**
- [XafGitHubCopilot](https://github.com/egarim/XafGitHubCopilot) - Original implementation

**Blog Posts:**
- [The Day I Integrated GitHub Copilot SDK Inside My XAF App — Part 1](https://www.jocheojeda.com/2026/02/16/the-day-i-integrated-github-copilot-sdk-inside-my-xaf-app-part-1/)
- [The Day I Integrated GitHub Copilot SDK Inside My XAF App — Part 2](https://www.jocheojeda.com/2026/02/16/the-day-i-integrated-github-copilot-sdk-inside-my-xaf-app-part-2/)

**DevExpress:**
- [DevExpress XAF](https://www.devexpress.com/xaf)
- [DevExpress AI Integration](https://www.devexpress.com/blazor/ai-integration/)

## ⚠️ DevExpress License Required

This project uses DevExpress XAF and AI components which require a commercial license.

**To build this project:**
1. Download your DevExpress license key from devexpress.com/DX1001
2. Place it in:
   - Windows: `%AppData%\DevExpress\DevExpress_License.txt`
   - macOS: `$HOME/Library/Application Support/DevExpress/DevExpress_License.txt`
   - Linux: `$HOME/.config/DevExpress/DevExpress_License.txt`

**Trial version:**
- Get a 30-day trial: https://www.devexpress.com/downloads/

## 📝 License

This is a demonstration project. See DevExpress licensing for commercial use.

## 🤝 Contributing

This is a pattern demonstration. For production use:
1. Fork this repository
2. Customize for your domain model
3. Add URL page implementation
4. Add REST API endpoints

## 📧 Author

**Jose Ojeda (Joche)**  
- Website: https://www.jocheojeda.com
- GitHub: https://github.com/egarim

---

**Status:** ✅ Working code, ready to adapt
