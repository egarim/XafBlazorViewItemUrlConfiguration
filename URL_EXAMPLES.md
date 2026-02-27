# URL Access Examples

## 🎯 Three Ways to Access the Chat ViewItem

### 1️⃣ Traditional XAF (Login Required)

**URL:** `https://localhost:5001`

**Steps:**
1. Browse to the app
2. Login: `Admin` / _(empty password)_
3. Navigate to **CopilotChat** in the menu
4. Use the ViewItem inside XAF

**Use case:** Internal business app with full XAF security

---

### 2️⃣ Direct URL Access (No Login)

The chat is now accessible via direct URLs with parameters!

#### Basic Access
```
https://localhost:5001/chat
```

#### With Route Parameter
```
https://localhost:5001/chat/Show me today's orders
```

#### With Query Parameters (Recommended)
```
https://localhost:5001/chat?message=Show me top 10 customers
https://localhost:5001/chat?message=List all products&context=inventory
```

#### Multiple Parameters
```
https://localhost:5001/chat?message=Order status&context=order123
```

**Use cases:**
- QR codes on receipts
- Email action links
- Bookmarkable queries
- Mobile app deep linking

---

### 3️⃣ REST API (Programmatic Access)

#### Health Check
```bash
curl https://localhost:5001/api/chat/health
```

**Response:**
```json
{
  "status": "ready",
  "timestamp": "2026-02-27T10:00:00Z",
  "service": "XAF Blazor Chat API"
}
```

#### Send Chat Message
```bash
curl -X POST https://localhost:5001/api/chat \
  -H "Content-Type: application/json" \
  -d '{
    "message": "Show me today'\''s orders",
    "context": {
      "userId": "123",
      "department": "sales"
    }
  }'
```

**Response:**
```json
{
  "message": "Here are today's orders: ...",
  "timestamp": "2026-02-27T10:00:00Z",
  "context": {
    "userId": "123",
    "department": "sales"
  }
}
```

**Use cases:**
- Mobile apps
- External systems
- Telegram/Discord bots
- Automated workflows

---

## 🚀 Testing Locally

### Start the Server

```bash
cd XafBlazorViewItemUrlConfiguration/XafBlazorViewItemUrlConfiguration.Blazor.Server
dotnet run
```

### Test Direct URLs

Open in browser:
```
https://localhost:5001/chat
https://localhost:5001/chat?message=Hello
```

### Test API

```bash
# Health check
curl https://localhost:5001/api/chat/health

# Send message
curl -X POST https://localhost:5001/api/chat \
  -H "Content-Type: application/json" \
  -d '{"message": "Show me products"}'
```

---

## 📱 Real-World Examples

### 1. QR Code on Receipt

**Print QR code encoding:**
```
https://yourapp.com/chat?message=Track my order&context=order-12345
```

Customer scans → Instant order tracking chat

### 2. Email Action Link

**Email body:**
```
Your order #789 is ready!
[Chat with AI about this order](https://yourapp.com/chat?message=Tell me about order 789)
```

### 3. Mobile App Integration

**Swift (iOS):**
```swift
let url = URL(string: "https://yourapp.com/chat?message=\(encodedQuery)")
webView.load(URLRequest(url: url!))
```

**Kotlin (Android):**
```kotlin
val url = "https://yourapp.com/chat?message=${URLEncoder.encode(query)}"
webView.loadUrl(url)
```

### 4. Telegram Bot

**Python:**
```python
import requests

response = requests.post(
    "https://yourapp.com/api/chat",
    json={"message": user_query}
)
bot.send_message(chat_id, response.json()["message"])
```

---

## 🔒 Security Considerations

### Current Setup (Demo)
- ❌ **No authentication** on `/chat` page
- ❌ **No authentication** on `/api/chat` endpoint

### Production Recommendations

#### Option 1: Add Authentication
```csharp
// Pages/Chat.razor
@attribute [Authorize]
```

#### Option 2: Token-Based Access
```csharp
// Controllers/ChatController.cs
[ApiKey] // Custom attribute
public class ChatController : ControllerBase
```

#### Option 3: Rate Limiting
```csharp
services.AddRateLimiter(options => {
    options.AddFixedWindowLimiter("chat", opt => {
        opt.Window = TimeSpan.FromMinutes(1);
        opt.PermitLimit = 10;
    });
});
```

---

## ✅ What's Working Now

| Feature | Status | URL |
|---|---|---|
| XAF ViewItem | ✅ Working | Login → Navigate |
| Direct URL | ✅ **NEW!** | `/chat?message=...` |
| REST API | ✅ **NEW!** | `POST /api/chat` |
| Health Check | ✅ **NEW!** | `GET /api/chat/health` |

---

## 🎯 Next Steps

1. **Deploy** to production server
2. **Add authentication** to `/chat` page
3. **Add API keys** for `/api/chat`
4. **Configure CORS** for mobile apps
5. **Add rate limiting**
6. **Monitor usage**

---

**Ready to use!** 🚀
