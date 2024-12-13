# SalesOrderProcessor

## Overview
The **SalesOrderProcessor** is a C# console application designed to simulate a typical use case in Dynamics 365 Business Central. It processes sales orders from a JSON file, applies business rules, and outputs the processed results in both JSON format and a console summary table.

---

## Features
1. **Input Handling**:
   - Reads a JSON file containing sales orders.
   - Validates input for missing or invalid data.

2. **Business Logic**:
   - Computes the subtotal for each order as the sum of `quantity * unit_price` for all items.
   - Applies a 10% discount if the subtotal exceeds $500.
   - Adds 7.5% VAT to the final discounted total.

3. **Output**:
   - Saves the processed orders to an `output.json` file.
   - Prints a summary table to the console showing key details of each order.

4. **Error Handling**:
   - Handles file not found, invalid JSON, and other runtime exceptions gracefully.

---

## Approach
1. **Input Parsing**:
   - Read the JSON file using `System.Text.Json`.
   - Deserialize the JSON into a list of `Order` objects.

2. **Processing**:
   - Compute subtotals for each order using LINQ.
   - Apply discounts and calculate VAT based on business rules.
   - Store results in `ProcessedOrder` objects.

3. **Output**:
   - Serialize the processed orders into a JSON file.
   - Display a formatted summary table in the console.

4. **Error Handling**:
   - Validate input data and handle missing or invalid entries.
   - Use `try-catch` blocks to manage runtime exceptions.

---

## Technologies Used
- **C#**: Primary programming language.
- **.NET**: Framework for building the console application.
- **System.Text.Json**: JSON serialization and deserialization.

---

## Contact
For further inquiries or feedback, contact:
- **Name**: Jamin
- **Email**: jaminonuegbu@gmail.com

