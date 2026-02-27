# XAF Blazor ViewItem URL Configuration

**How to make XAF ViewItems accessible via direct URL with parameters in Blazor**

This project demonstrates how to expose XAF ViewItems as standalone pages with URL parameter configuration, plus REST API access - all using the same component.

## 🎯 The Problem This Solves

**Traditional XAF ViewItems:**
- ❌ Only accessible after login + navigation
- ❌ No direct URL access
- ❌ Can't share links to specific views
- ❌ No URL parameters

**This Solution:**
- ✅ Direct URL access: `/chat?message=Hello`
- ✅ URL parameters configure ViewItem state
- ✅ Shareable links
- ✅ Embeddable in other apps
- ✅ REST API for programmatic access

---

## 🚀 Three Integration Approaches

### 1️⃣ **Traditional XAF ViewItem** (Baseline)
**What:** Standard XAF ViewItem embedded in application
**Access:** Login → Navigate → View
**Use case:** Internal business app with full XAF security

```
https://yourapp.com/ → Login → Navigate to "AI Chat"
```

### 2️⃣ **URL-Configurable Standalone Page** (★ Main Feature)
**What:** Blazor page exposing ViewItem via direct URL
**Access:** Direct URL with parameters
**Use case:** Shareable links, bookmarks, QR codes, embeds

**Examples:**
```
https://yourapp.com/chat
https://yourapp.com/chat?message=Hello
https://yourapp.com/chat?message=Show order 123&context=order123
```

**URL Parameters:**
- `message` - Pre-populate initial message
- `context` - Pass context data (orderId, userId, etc.)

### 3️⃣ **REST API Endpoint** (Bonus)
**What:** HTTP API for programmatic access
**Access:** POST request with JSON
**Use case:** Mobile apps, external systems, automation

**Endpoint:**
```bash
POST https://yourapp.com/api/chat
{
  "message": "List all orders",
  "context": { "userId": "123" }
}
```

---

## 📖 Example: AI Chat ViewItem

This demo uses an **AI Chat component** (GitHub Copilot SDK) as the ViewItem example, but **the URL configuration pattern works for ANY XAF ViewItem**.

**The same approach can be used for:**
- Order detail views → `/order?id=123`
- Customer profiles → `/customer?id=456&tab=orders`
- Reports → `/report?type=sales&month=2026-02`
- Dashboard widgets → `/dashboard?widget=sales&period=today`

---

## 🏗️ How It Works

### Architecture

```
Traditional XAF ViewItem (secured, full XAF)
         ↓
    Shared Component
   (CopilotChat.razor)
         ↓
         ├──→ Standalone Page (/chat) ← URL Parameters
         └──→ REST API (/api/chat) ← JSON Request
```

### Key Components

**1. Shared ViewItem Component**
```
XafBlazorViewItemUrlConfiguration.Blazor.Server/
└── Editors/CopilotChatViewItem/
    ├── CopilotChat.razor              ← Reusable component
    └── CopilotChatViewItemBlazor.cs   ← XAF ViewItem wrapper
```

**2. Standalone Page with URL Parameters**
```
XafBlazorViewItemUrlConfiguration.Blazor.Server/
└── Pages/
    └── Chat.razor                     ← URL-accessible page
```

**Code snippet:**
```csharp
@page "/chat"
@page "/chat/{InitialMessage?}"

@code {
    [Parameter]
    [SupplyParameterFromQuery(Name = "message")]
    public string? InitialMessage { get; set; }

    [Parameter]
    [SupplyParameterFromQuery(Name = "context")]
    public string? Context { get; set; }
}
```

**3. REST API Controller**
```
XafBlazorViewItemUrlConfiguration.WebApi/
└── Controllers/
    └── ChatController.cs              ← HTTP endpoint
```

---

## 🚀 Quick Start

### Prerequisites
- .NET 9.0 SDK
- DevExpress XAF license
- GitHub Copilot API key (for this demo)

### 1. Clone & Restore
```bash
git clone https://github.com/egarim/XafBlazorViewItemUrlConfiguration.git
cd XafBlazorViewItemUrlConfiguration
dotnet restore XafBlazorViewItemUrlConfiguration.sln
```

### 2. Configure API Key
Edit `appsettings.json` in both projects:

```json
{
  "GitHubCopilot": {
    "ApiKey": "your-copilot-api-key",
    "Model": "gpt-4o"
  }
}
```

### 3. Run
```bash
# Terminal 1: Blazor Server (port 5001)
cd XafBlazorViewItemUrlConfiguration.Blazor.Server
dotnet run

# Terminal 2: Web API (port 5002)
cd XafBlazorViewItemUrlConfiguration.WebApi
dotnet run
```

### 4. Test

**Traditional XAF:**
```
https://localhost:5001
Login: Admin / (empty password)
Navigate to: "Copilot Chat"
```

**Direct URL Access:**
```
https://localhost:5001/chat
https://localhost:5001/chat?message=Hello
https://localhost:5001/chat?message=Show order&context=order123
```

**REST API:**
```bash
curl -X POST https://localhost:5002/api/chat \
  -H "Content-Type: application/json" \
  -d '{"message": "Hello, who are you?"}'
```

---

## 🎨 Adapting for Your ViewItems

### Step 1: Extract Your ViewItem as a Component

**Before (XAF-only):**
```csharp
[ViewItem(typeof(IModelMyViewItem))]
public class MyViewItemBlazor : ViewItem
{
    // XAF-specific code
}
```

**After (Reusable Component):**
```csharp
// 1. Create standalone component
// MyComponent.razor
<div class="my-component">
    @* Your ViewItem UI *@
</div>

@code {
    [Parameter]
    public string? InitialData { get; set; }
}

// 2. Wrap in XAF ViewItem
public class MyViewItemBlazor : ViewItem, IComponentContentHolder
{
    RenderFragment IComponentContentHolder.ComponentContent => builder =>
    {
        builder.OpenComponent<MyComponent>(0);
        builder.CloseComponent();
    };
}
```

### Step 2: Create Standalone Page

```csharp
// Pages/MyView.razor
@page "/myview"
@page "/myview/{Id?}"

<MyComponent InitialData="@Id" />

@code {
    [Parameter]
    [SupplyParameterFromQuery(Name = "id")]
    public string? Id { get; set; }
}
```

### Step 3: Add API Endpoint (Optional)

```csharp
[ApiController]
[Route("api/myview")]
public class MyViewController : ControllerBase
{
    [HttpPost]
    public IActionResult Process([FromBody] MyRequest request)
    {
        // Process and return data
        return Ok(new MyResponse { ... });
    }
}
```

---

## 📊 Comparison: Before vs After

| Feature | Traditional XAF | With URL Config |
|---|---|---|
| **Login Required** | ✅ Always | ⚙️ Optional |
| **Direct URL Access** | ❌ No | ✅ Yes |
| **Shareable Links** | ❌ No | ✅ Yes |
| **URL Parameters** | ❌ No | ✅ Yes |
| **Embeddable** | ❌ No | ✅ Yes |
| **REST API** | ❌ No | ✅ Yes |
| **XAF Security** | ✅ Full | ⚙️ Optional |

---

## 🎯 Real-World Use Cases

### 1. Customer Order Links
**Problem:** Customer calls: "Where's my order?"
**Solution:** Send them a direct link

```
https://yourapp.com/order?id=12345&email=customer@example.com
```

Customer sees order status without login.

### 2. QR Code on Receipts
**Problem:** Want customers to access support chat
**Solution:** Print QR code linking to chat with order context

```
https://yourapp.com/support?order=12345&store=downtown
```

### 3. Email Action Links
**Problem:** Email says "Approve expense report #789"
**Solution:** Link directly to approval view

```
https://yourapp.com/approve?type=expense&id=789&token=...
```

### 4. Mobile App Integration
**Problem:** Mobile app needs to access XAF functionality
**Solution:** Call REST API

```csharp
var response = await httpClient.PostAsJsonAsync(
    "https://yourapp.com/api/order",
    new { orderId = 123 }
);
```

### 5. Telegram/Discord Bot
**Problem:** Users want to query data via chat
**Solution:** Bot calls your API

```python
response = requests.post("https://yourapp.com/api/chat", 
    json={"message": "Show today's orders"})
```

---

## 🔒 Security Considerations

### Public Access (Standalone Page)
```csharp
// Chat.razor - No authentication
@page "/chat"
<MyComponent />
```

### Token-Based Auth
```csharp
// Chat.razor - Require token
@page "/chat"
@attribute [Authorize]
<MyComponent />
```

### API Key Auth
```csharp
// ChatController.cs
[ApiKey] // Custom attribute
public class ChatController : ControllerBase
{
    // ...
}
```

### XAF Security Integration
```csharp
// Keep XAF security for ViewItem, public for standalone
if (User.Identity?.IsAuthenticated == true)
{
    // Use XAF security
}
else
{
    // Limited public access
}
```

---

## 📁 Project Structure

```
XafBlazorViewItemUrlConfiguration/
├── XafBlazorViewItemUrlConfiguration.Blazor.Server/
│   ├── Editors/
│   │   └── CopilotChatViewItem/
│   │       ├── CopilotChat.razor              ← Shared component
│   │       └── CopilotChatViewItemBlazor.cs   ← XAF ViewItem
│   └── Pages/
│       └── Chat.razor                          ← URL-accessible page ★
│
├── XafBlazorViewItemUrlConfiguration.WebApi/
│   └── Controllers/
│       └── ChatController.cs                   ← REST API ★
│
├── XafBlazorViewItemUrlConfiguration.Module/
│   └── Services/
│       └── CopilotChatService.cs              ← Business logic
│
└── README.md                                   ← You are here
```

---

## 🧪 Testing URLs

### Basic Access
```
https://localhost:5001/chat
```

### Pre-populated Message
```
https://localhost:5001/chat?message=List all orders
```

### With Context
```
https://localhost:5001/chat?message=Show details&context=order123
```

### Multiple Parameters
```
https://localhost:5001/chat?message=Hello&context=user:456,order:789
```

---

## 💡 Tips & Best Practices

### 1. URL-Friendly Parameter Encoding
```csharp
// Good
var url = $"/chat?message={Uri.EscapeDataString("Show order #123")}";
// Result: /chat?message=Show%20order%20%23123

// Bad
var url = $"/chat?message=Show order #123"; // Breaks with special chars
```

### 2. Deep Linking Pattern
```csharp
// Support both formats
@page "/order/{Id}"                    // /order/123
[SupplyParameterFromQuery(Name = "id")] // /order?id=123
```

### 3. State Preservation
```csharp
// Save state to URL when it changes
protected override void OnParametersSet()
{
    NavigationManager.NavigateTo(
        $"/chat?message={CurrentMessage}&context={CurrentContext}",
        replace: true
    );
}
```

### 4. Share Button Implementation
```razor
<button @onclick="CopyShareLink">📋 Copy Link</button>

@code {
    async Task CopyShareLink()
    {
        var url = $"{NavigationManager.BaseUri}chat?message={Message}";
        await JSRuntime.InvokeVoidAsync("navigator.clipboard.writeText", url);
    }
}
```

---

## 📚 Related Articles

This project is inspired by:
- [The Day I Integrated GitHub Copilot SDK Inside My XAF App — Part 1](https://www.jocheojeda.com/2026/02/16/the-day-i-integrated-github-copilot-sdk-inside-my-xaf-app-part-1/)
- [The Day I Integrated GitHub Copilot SDK Inside My XAF App — Part 2](https://www.jocheojeda.com/2026/02/16/the-day-i-integrated-github-copilot-sdk-inside-my-xaf-app-part-2/)

---

## 🤝 Contributing

Contributions welcome! Open an issue or PR.

---

## 📄 License

MIT License - see LICENSE.txt

---

## 🔗 Links

- [DevExpress XAF](https://www.devexpress.com/xaf)
- [Blazor Routing](https://learn.microsoft.com/en-us/aspnet/core/blazor/fundamentals/routing)
- [GitHub Copilot SDK](https://github.com/features/copilot)

---

**Made with ❤️ by [Joche Ojeda](https://www.jocheojeda.com)**

**Tags:** `xaf`, `devexpress`, `blazor`, `url-configuration`, `viewitem`, `deep-linking`, `rest-api`, `copilot`
