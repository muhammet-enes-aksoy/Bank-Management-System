# Customer Management System

## Overview

Customer Management System is a .NET Core application designed to manage customer information, addresses, contact details, and accounts. It provides a set of APIs for basic CRUD operations on entities such as Customer, Address, Contact, Account, Account Transaction, and EFT Transaction.

## API Endpoints

### Customers:

![Screenshot_8](https://github.com/300-Akbank-Net-Bootcamp/aw-3-muhammet-enes-aksoy/assets/97848966/36ca483a-05a4-419e-ba16-fc688a2bc28a)

- **GET /api/customers:**
  
- **GET /api/customers/{id}:**
  
- **GET /api/customers/parameter:**
  ![image](https://github.com/300-Akbank-Net-Bootcamp/aw-3-muhammet-enes-aksoy/assets/97848966/f3704fc4-d2e6-4b06-a120-fa48b4d022d3)

- **POST /api/customers:**
   ![Screenshot_0](https://github.com/300-Akbank-Net-Bootcamp/aw-3-muhammet-enes-aksoy/assets/97848966/d12d3ada-a66c-43fb-8a01-ed39c9ac1667)
   ![Screenshot_1](https://github.com/300-Akbank-Net-Bootcamp/aw-3-muhammet-enes-aksoy/assets/97848966/8fd4574b-7a44-4015-ba20-4fb7bf3503e5)
- **PUT /api/customers/{id}:**
  
- **DELETE /api/customers/{id}:**

### Database
![image](https://github.com/300-Akbank-Net-Bootcamp/aw-3-muhammet-enes-aksoy/assets/97848966/3abc52d8-461f-4399-8dcb-3139ba10417c)


### Addresses:

![Screenshot_6](https://github.com/300-Akbank-Net-Bootcamp/aw-3-muhammet-enes-aksoy/assets/97848966/fe793e8b-07ed-40cc-84e7-ca32e88f92e0)

- **GET /api/addresses:**
    ![image](https://github.com/300-Akbank-Net-Bootcamp/aw-3-muhammet-enes-aksoy/assets/97848966/4df2a9eb-4e2f-4f22-a4cd-09def4f63b11)

- **GET /api/addresses/{id}:**
  ![image](https://github.com/300-Akbank-Net-Bootcamp/aw-3-muhammet-enes-aksoy/assets/97848966/8974c1cd-ace7-41fd-a123-5a82ea4eb9f5)

- **GET /api/addresses/parameter:**
  
- **POST /api/addresses:**
   ![Screenshot_2](https://github.com/300-Akbank-Net-Bootcamp/aw-3-muhammet-enes-aksoy/assets/97848966/16d27988-c8d2-4f7f-bfc8-839ee845d5e8)
  
- **PUT /api/addresses/{id}:**
  
- **DELETE /api/addresses/{id}:**
  
### Database
![image](https://github.com/300-Akbank-Net-Bootcamp/aw-3-muhammet-enes-aksoy/assets/97848966/61e0fac0-5e7e-4752-8537-d843bfecee59)


### Contacts:

![Screenshot_7](https://github.com/300-Akbank-Net-Bootcamp/aw-3-muhammet-enes-aksoy/assets/97848966/10ba8ea6-e006-442b-854a-ca34596cd938)

- **GET /api/contacts:**
  ![image](https://github.com/300-Akbank-Net-Bootcamp/aw-3-muhammet-enes-aksoy/assets/97848966/2af9429b-7327-44d2-8419-552f0dd3b0da)

- **GET /api/contacts/{id}:**
  
- **GET /api/contacts/parameter:**
  
- **POST /api/contacts:**
   ![Screenshot_3](https://github.com/300-Akbank-Net-Bootcamp/aw-3-muhammet-enes-aksoy/assets/97848966/fff95810-b084-4c31-ad59-0c0a9a01c699)

- **PUT /api/contacts/{id}:**
  
- **DELETE /api/contacts/{id}:**
  
### Database
![image](https://github.com/300-Akbank-Net-Bootcamp/aw-3-muhammet-enes-aksoy/assets/97848966/b6c51e69-8fee-41e2-8d67-79bbbc4a8000)
![image](https://github.com/300-Akbank-Net-Bootcamp/aw-3-muhammet-enes-aksoy/assets/97848966/3cec0fdc-f16b-4e07-bc88-864d2af6ae5a)


### Accounts:

![Screenshot_4](https://github.com/300-Akbank-Net-Bootcamp/aw-3-muhammet-enes-aksoy/assets/97848966/d990c146-ad58-44cb-a651-f0247690ddf2)

- **GET /api/accounts:**
    ![image](https://github.com/300-Akbank-Net-Bootcamp/aw-3-muhammet-enes-aksoy/assets/97848966/a3c10c32-29ab-4564-ab24-61290c1955d0)


- **GET /api/accounts/{id}:**
  
- **GET /api/accounts/parameter:**
  
- **POST /api/accounts:**
  ![Screenshot_3](https://github.com/300-Akbank-Net-Bootcamp/aw-3-muhammet-enes-aksoy/assets/97848966/87422f1e-3959-4dd2-bfbc-41d127021331)
 
- **PUT /api/accounts/{id}:**
  
- **DELETE /api/accounts/{id}:**
    ![image](https://github.com/300-Akbank-Net-Bootcamp/aw-3-muhammet-enes-aksoy/assets/97848966/c19e208b-0df8-40b9-89a2-ff26ff1ac5a4)

### Database
![image](https://github.com/300-Akbank-Net-Bootcamp/aw-3-muhammet-enes-aksoy/assets/97848966/ba2b9569-0099-483e-b9db-454b3ed7ba2b)


### Account Transactions:

![Screenshot_5](https://github.com/300-Akbank-Net-Bootcamp/aw-3-muhammet-enes-aksoy/assets/97848966/06fc6a53-e427-40fa-86f2-2ed13e38bc55)

- **GET /api/accounttransactions:**
  
- **GET /api/accounttransactions/{id}:**
  
- **GET /api/accounttransactions/parameter:**
   
- **POST /api/accounttransactions:**
  
- **PUT /api/accounttransactions/{id}:**
  
- **DELETE /api/accounttransactions/{id}:**


### EFT Transactions:

![Screenshot_9](https://github.com/300-Akbank-Net-Bootcamp/aw-3-muhammet-enes-aksoy/assets/97848966/bcb9f4d2-7321-41a3-b79b-0f161fbb33d9)

- **GET /api/efttransactions:**
- **GET /api/efttransactions/{id}:**    
- **GET /api/efttransactions/parameter:**  
- **POST /api/efttransactions:**
- **PUT /api/efttransactions/{id}:**
- **DELETE /api/efttransactions/{id}:**


## Technologies Used

- **ASP.NET Core Web API:** Framework for building HTTP services.
- **Entity Framework Core:** Object-Relational Mapping (ORM) for database interaction.
- **C# Programming Language:** Primary language for development.
- **Microsoft SQL Server:** Database for storing customer and transactional data.
