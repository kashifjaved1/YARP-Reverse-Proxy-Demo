# YARP Reverse Proxy Demo

![.NET](https://img.shields.io/badge/.NET-8.0-blue)
![YARP](https://img.shields.io/badge/YARP-2.3.0-green)
![Microservices](https://img.shields.io/badge/Architecture-Microservices-orange)

## Solution Structure

```text
YarpReverseProxyDemo/
├── src/
│   ├── ApiGateway/
│   ├── Services/
│   │   ├── Order.Api/
│   │   ├── Product.Api/
│   │   └── User.Api/
└── README.md
```

### Session Affinity (Sticky Sessions)

| Aspect                | Details                                                                 |
|-----------------------|-------------------------------------------------------------------------|
| **Definition**         | Ensures client requests are routed to the same backend server during a session |
| **Use Cases**          | - Stateful applications <br>- Shopping carts <br>- Multi-step workflows |
| **Implementation**     | Cookie-based or custom header affinity                                 |
| **Failure Handling**   | `Redistribute` (default) or `Return503Error`                           |

#### Cookie-Based Affinity Configuration
```json
"SessionAffinity": {
  "Enabled": true, // true|false, defaults to 'false'
  "Policy": "Cookie", // HashCookie|ArrCookie|Cookie|CustomHeader, defaults to 'HashCookie'
  "FailurePolicy": "Redistribute", // Redistribute|Return503Error, defaults to 'Redistribute'
  "AffinityKeyName": "MyAffinityKey",
  "Cookie": {
    "Domain": "localhost",
    "Expiration": "03:00:00",
    "HttpOnly": true,
    "IsEssential": true,
    "MaxAge": "1.00:00:00",
    "Path": "mypath",
    "SameSite": "Strict",
    "SecurePolicy": "Always"
  }
}
```

## Affinity Flow

1. **Initial Request**  
   ○ No cookie → Load balancer selects backend  
   ○ Response includes `Set-Cookie` header  

2. **Subsequent Requests**  
   ○ Cookie present → Route to same backend  
   ○ Backend unavailable → Apply failure policy  

---

## Service Port Mapping

| Service           | Port   | Protocol    | Health Check Endpoint |
|-------------------|--------|-------------|-----------------------|
| YARP Gateway      | 5001   | HTTP/HTTPS  | N/A                   |
| Product Service   | 3001   | HTTP        | `/health`             |
| Order Service     | 3002   | HTTP        | `/health`             |
| User Service      | 3003   | HTTP        | `/health`             |

## Load Balancing Implementation

### Policy Types

| Policy               | Algorithm                          | Use Case                |
|----------------------|------------------------------------|-------------------------|
| **RoundRobin**       | Cyclic distribution                | Simple workloads        |
| **PowerOfTwoChoices**| Selects 2 random, picks least busy | General purpose         |
| **LeastRequests**    | Routes to server with fewest active requests | Variable workloads |
| **Random**           | Random selection                   | Testing/Development     |
