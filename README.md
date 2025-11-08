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


This will launch all three APIs. Check the terminal output for the specific URLs for each service. By default, they are usually:
- **TransactionAPI**: `http://localhost:5002`
- **DiscountAPI**: `http://localhost:5013`
- **LoyaltyAPI**: `http://localhost:5153`


If you are using Visual Studio, the corresponding `.http` files are provided for quickly checking that the endpoints are working.
