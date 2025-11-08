# Ampol Digital Solution

This solution contains three separate APIs:
- **DiscountAPI**: Handles discount calculations.
- **LoyaltyAPI**: Manages loyalty points.
- **TransactionAPI**: Processes transactions, coordinating with the Discount and Loyalty APIs.

## Prerequisites

- .NET 9 SDK

## How to Build

To build the entire solution, run the following command from the root directory:

```bash
dotnet build
```

## How to Run

You can run all APIs at once using the solution file.

```bash
dotnet run --project src/TransactionAPI/TransactionAPI.csproj
```

This will launch all three APIs. Check the terminal output for the specific URLs for each service. By default, they are usually:
- **TransactionAPI**: `http://localhost:5002`
- **DiscountAPI**: `http://localhost:5013`
- **LoyaltyAPI**: `http://localhost:5153`

Alternatively, you can run each API independently from the command line. Open a separate terminal for each API.

### 1. Run DiscountAPI

```bash
dotnet run --project src/DiscountAPI/DiscountAPI.csproj
```

### 2. Run LoyaltyAPI

```bash
dotnet run --project src/LoyaltyAPI/LoyaltyAPI.csproj
```

### 3. Run TransactionAPI

```bash
dotnet run --project src/TransactionAPI/TransactionAPI.csproj
```

## Sample Transaction Payload

```json
{
  "CustomerId": "8e4e8991-aaee-495b-9f24-52d5d0e509c5",
  "LoyaltyCard": "CTX0000001",
  "TransactionDate": "03-Apr-2020",
  "Basket": [
    {
      "ProductId": "PRD01",
      "UnitPrice": "1.2",
      "Quantity": "3"
    },
    {
      "ProductId": "PRD02",
      "UnitPrice": "2.0",
      "Quantity": "2"
    },
    {
      "ProductId": "PRD03",
      "UnitPrice": "5.0",
      "Quantity": "1"
    }
  ]
}
```