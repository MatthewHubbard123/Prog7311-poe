# Agri-Energy Connect

Agri-Energy Connect is an ASP.NET MVC web application that bridges the gap between *farmers* and *employees* in the agri-energy industry. It enables efficient management of agricultural products by allowing 
different roles to perform dedicated tasks such as product addition, filtering, and account management.

## 🌾 Project Overview

This platform allows:
- *Farmers* to:
  - Register and log in
  - Add products they’ve produced
  - View a list of their own added products

- *Employees* to:
  - Register and log in
  - Add new products on behalf of farmers
  - Add farmer information
  - Filter products using:
    - `FarmerName`
    - `ProductName`
    - `Category`
    - `ProductionDate`
    - The filter returns all product information related to the search

The system includes:
- A fully functional **Login**, **Register**, and **Logout** system
- Support for switching between multiple user accounts
- A local **SQLite database** for easy and lightweight storage

---------------------------------------------------------------------------------------------------

🚀 Getting Started

Follow the steps below to run the project locally on your machine.

#Prerequisites

- [Application Used - Visual Studio Code] with the ASP.NET and web development workload
- [Targeted framework(.NET 8.0)]
- [.NET SDK](https://dotnet.microsoft.com/en-us/download) (Using .NET SDK version 9.0.102.)
- [SQLite tools](https://www.sqlite.org/download.html) (optional, for inspecting the database)

---------------------------------------------------------------------------------------------------

⚙️ Setup Instructions

1. **Clone the Repository**

   ```bash```
   git clone https://github.com/MatthewHubbard123/Prog7311-poe.git
   cd Prog7311 Poe
