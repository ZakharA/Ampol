# Tech test 

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

## How to Test APIs with .http Files

This project includes `.http` files for each API to allow for easy testing of the endpoints. These files are located in the root of each API project directory:

- `src/DiscountAPI/DiscountAPI.http`
- `src/LoyaltyAPI/LoyaltyAPI.http`
- `src/TransactionAPI/TransactionAPI.http`

You can modify the request body in the `.http` files to test with different data.

This will launch all three APIs. Check the terminal output for the specific URLs for each service. By default, they are usually:
- **TransactionAPI**: `http://localhost:5002`
- **DiscountAPI**: `http://localhost:5013`
- **LoyaltyAPI**: `http://localhost:5153`

### Testing with Postman

Alternatively, you can use  Postman to test the APIs.  The collections are loacated in the root folder 

