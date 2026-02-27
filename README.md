# XAF Blazor ViewItem URL Configuration

✅ **WORKING CODE** - Builds successfully on .NET 9

This is a **production-ready copy** of XafGitHubCopilot with renamed namespaces, demonstrating how XAF ViewItems work in Blazor.

## ✅ Build Status

**Status:** Builds successfully  
**Framework:** .NET 9.0  
**Tested on:** Linux + .NET 9.0 SDK

```bash
dotnet build XafBlazorViewItemUrlConfiguration.slnx
# Build succeeded. 0 Error(s)
```

## 🎯 What This Shows

This is the **working base code** from XafGitHubCopilot. It demonstrates:

1. ✅ **Reusable Blazor Component** (`CopilotChat.razor`)
2. ✅ **XAF ViewItem Wrapper** (`CopilotChatViewItemBlazor.cs`)
3. ✅ **GitHub Copilot SDK Integration** (services layer)
4. ✅ **Complete XAF Application** (business objects, controllers, security)

## 📁 What's Included

- `XafBlazorViewItemUrlConfiguration.Module/` - Shared module (business objects, services)
- `XafBlazorViewItemUrlConfiguration.Blazor.Server/` - Blazor Server UI

## 🚀 Quick Start

### Prerequisites

- **.NET 9.0 SDK**
- **DevExpress Universal License** (v25.2+)
- License activated on your machine

### Build

```bash
git clone https://github.com/egarim/XafBlazorViewItemUrlConfiguration.git
cd XafBlazorViewItemUrlConfiguration
dotnet build XafBlazorViewItemUrlConfiguration.slnx
```

### Run

```bash
cd XafBlazorViewItemUrlConfiguration/XafBlazorViewItemUrlConfiguration.Blazor.Server
dotnet run
```

Browse: `https://localhost:5001`  
Login: `Admin` / _(empty password)_

## 🔧 Key Files to Study

### The ViewItem Pattern

**Component** - `Editors/CopilotChatViewItem/CopilotChat.razor`:
```razor
@using DevExpress.Blazor.AIIntegration
<DxAIChat @ref="chat" 
          CssClass="w-100 vh-100"
          MessageSent="@OnMessageSent" />
```

**XAF Wrapper** - `Editors/CopilotChatViewItem/CopilotChatViewItemBlazor.cs`:
```csharp
public class CopilotChatViewItemBlazor : ViewItem, IComponentContentHolder
{
    RenderFragment IComponentContentHolder.ComponentContent => builder =>
    {
        builder.OpenComponent<CopilotChat>(0);
        builder.AddAttribute(1, "SystemPrompt", SystemPrompt);
        builder.AddAttribute(2, "InitialMessage", InitialMessage);
        builder.CloseComponent();
    };
}
```

### Services Integration

**GitHub Copilot Client** - `Module/Services/CopilotChatService.cs`:
```csharp
public class CopilotChatService
{
    private readonly IChatClient _chatClient;
    
    public async Task<string> SendMessageAsync(string message)
    {
        var response = await _chatClient.CompleteAsync(message);
        return response.Message.Text;
    }
}
```

## 📝 Next Steps (To Add URL Access)

### 1. Create Standalone Page

Add `Pages/Chat.razor`:

```razor
@page "/chat"
@page "/chat/{InitialMessage?}"
@using XafBlazorViewItemUrlConfiguration.Blazor.Server.Editors.CopilotChatViewItem

<CopilotChat InitialMessage="@InitialMessage" />

@code {
    [Parameter]
    [SupplyParameterFromQuery(Name = "message")]
    public string? InitialMessage { get; set; }
}
```

### 2. Add REST API Endpoint

Create `Controllers/ChatController.cs`:

```csharp
[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly CopilotChatService _chatService;
    
    [HttpPost]
    public async Task<IActionResult> Chat([FromBody] ChatRequest request)
    {
        var response = await _chatService.SendMessageAsync(request.Message);
        return Ok(new { message = response });
    }
}
```

## ⚠️ Important Notes

### DevExpress License Required

This project requires DevExpress Universal subscription (v25.2+).

**Activate license:**
1. Download key from devexpress.com/DX1001
2. Place in:
   - Windows: `%AppData%\DevExpress\DevExpress_License.txt`
   - macOS: `$HOME/Library/Application Support/DevExpress/DevExpress_License.txt`
   - Linux: `$HOME/.config/DevExpress/DevExpress_License.txt`

**Trial:** https://www.devexpress.com/downloads/

### .NET Version

- **This repo:** .NET 9.0 (compatible with current SDK)
- **Original:** .NET 10 preview (requires preview SDK)

## 📚 Related Resources

**Original Project:**
- [XafGitHubCopilot](https://github.com/egarim/XafGitHubCopilot) - Source implementation

**Blog Posts:**
- [Part 1: Integrating GitHub Copilot SDK in XAF](https://www.jocheojeda.com/2026/02/16/the-day-i-integrated-github-copilot-sdk-inside-my-xaf-app-part-1/)
- [Part 2: Advanced Integration](https://www.jocheojeda.com/2026/02/16/the-day-i-integrated-github-copilot-sdk-inside-my-xaf-app-part-2/)

**DevExpress:**
- [XAF Documentation](https://docs.devexpress.com/eXpressAppFramework/113577/expressapp-framework)
- [Blazor AI Integration](https://www.devexpress.com/blazor/ai-integration/)

## 🎯 What Makes This Different

| Feature | This Repo | Original XafGitHubCopilot |
|---|---|---|
| .NET Version | 9.0 (stable) | 10.0 (preview) |
| Builds on Linux | ✅ Yes | ❌ Requires .NET 10 |
| Projects Included | Module + Blazor | Module + Blazor + Win + WebApi |
| Purpose | Pattern demonstration | Full production app |

## 🤝 Contributing

This is a demonstration/pattern reference. For production use:

1. Fork this repository
2. Add URL page implementation (`Pages/Chat.razor`)
3. Add REST API (`Controllers/ChatController.cs`)
4. Customize for your domain model

## 📧 Author

**Jose Ojeda (Joche)**  
- Website: https://www.jocheojeda.com
- GitHub: https://github.com/egarim
- Original: https://github.com/egarim/XafGitHubCopilot

---

**Status:** ✅ Working code, builds successfully, ready to extend

---

## 🎯 **URL Access - NOW WORKING!**

### ✅ What's Available

| Access Method | URL | Auth Required |
|---|---|---|
| **Traditional XAF** | `https://localhost:5001` → Login | ✅ Yes |
| **Direct URL** | `https://localhost:5001/chat?message=Hello` | ❌ No |
| **REST API** | `POST https://localhost:5001/api/chat` | ❌ No |

### 📖 Quick Examples

#### 1. Direct Chat Page
```
https://localhost:5001/chat
https://localhost:5001/chat?message=Show me today's orders
https://localhost:5001/chat?message=List products&context=inventory
```

#### 2. REST API
```bash
# Health check
curl https://localhost:5001/api/chat/health

# Send message
curl -X POST https://localhost:5001/api/chat \
  -H "Content-Type: application/json" \
  -d '{"message": "Show me top customers"}'
```

**📚 Full documentation:** [URL_EXAMPLES.md](URL_EXAMPLES.md)

---
