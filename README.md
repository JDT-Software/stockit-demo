# Stockit - Inventory Management System

A modern inventory management system built with Blazor WebAssembly and .NET 9.

## Demo Access

When you first open the application, you'll be presented with a login screen. Use the following credentials to access the demo:

### Admin Account
- **Username:** `admin`
- **Password:** `admin123`
- **Access:** Full system access including dashboard, products, suppliers, purchase orders, and stock management

### Floor Supervisor Account
- **Username:** `supervisor`
- **Password:** `super123`
- **Access:** Limited to stock entry and basic operations

## Features

- **Dashboard Analytics** - Visual insights into stock movements, category distribution, and trends
- **Product Management** - Comprehensive product catalog with categories and stock tracking
- **Supplier Management** - Maintain supplier information and contact details
- **Purchase Orders** - Create and manage purchase orders with document attachments
- **Stock Management** - Track stock movements, adjustments, damages, and returns
- **Low Stock Alerts** - Automatic alerts for products below reorder point with quick ordering
- **Role-Based Access** - Different permissions for administrators and floor staff

## Running the Application

### Prerequisites
- .NET 9 SDK
- Modern web browser (Chrome, Edge, Firefox)
- Backend API server running (default: http://localhost:5041)

### Steps to Run

1. **Ensure the backend API is running** on port 5041
   
2. **Run the Blazor WebAssembly app:**
   ```powershell
   dotnet run
   ```

3. **Open your browser** and navigate to the local development URL (typically https://localhost:5001 or http://localhost:5000)

4. **Login** using one of the demo credentials above

## Technology Stack

- **Frontend:** Blazor WebAssembly (.NET 9)
- **UI Framework:** MudBlazor
- **State Management:** Fluxor
- **Charts:** ApexCharts
- **Storage:** Browser LocalStorage for authentication persistence

## Project Structure

- `/Components` - Reusable UI components and dialogs
- `/Pages` - Main application pages
- `/Services` - API communication and business logic
- `/Models` - Data models and DTOs
- `/State` - Fluxor state management
- `/Layout` - Application layout components

## Notes

- The application persists authentication in browser LocalStorage, so you'll remain logged in across sessions
- All data is managed through the backend API
- Stock alerts are calculated in real-time based on current inventory levels vs. reorder points

## Support

For issues or questions, please refer to the project documentation or contact the development team.
