# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/) and this project loosely adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html)

---

## NOT YET RELEASED

### New

- TBA.

### Change

- TBA.

### Fix

- TBA.

---

## 1.14.0 (2024-08-17)

### New

- Warlock MVC. Add user management functionality.

---

## 1.13.1 (2024-08-17)

### Fix

- Warlock MVC. Refactor hardcoded doamin to dynamic request based value.

---

## 1.13.0 (2024-08-17)

### New

- Warlock DataAccess. Create DbInitializer.

### Change

- Warlock MVC. Update program to use DbInitializer.

---

## 1.12.0 (2024-08-17)

### New

- Warlock MVC. Add product counter to cart icon using session.
- Warlock MVC. Add Facebook login.
- Warlock MVC. Add `Create User` page for admin.

### Change

- Warlock MVC. Restore authorized access.
- Warlock MVC. Update packages.

---

## 1.11.0 (2024-08-16)

### New

- Warlock MVC. Implement order management functionality.

---

## 1.10.0 (2024-08-09)

### New

- Warlock Model. Add `ShoppingCart` model.
- Warlock DataAccess. Add `ShoppingCart` model to DbContext.
- Warlock MVC. Implement cart functionality.

### Change

- Warlock DataAccess. Update repository methods to configure tracking.
- Warlock MVC. Update product details page to use `ShoppingCart` model.

### Fix

- Warlock MVC. Fix Razor runtime compilation.

---

## 1.9.0 (2024-08-07)

### New

- Warlock Model. Add `Faction` model.
- Warlock DataAccess. Add `Faction` model to DbContext, Add seed data.
- Warlock MVC. Create `Faction` CRUD.

### Change

- Warlock. Update role names.
- Warlock MVC. Disable role based access.
- Warlock MVC. Add faction support to user registration.

### Fix

- Warlock MVC. Product. Fix URL parameter in product edit button.
- Warlock MVC. Fix null reference warning in product controller.

---

## 1.8.0 (2024-08-04)

### New

- Warlock MVC. Add Identity based login management.

---

## 1.7.0 (2024-08-03)

### New

- Warlock MVC. Products. Update homepage with product listing and add product details page.

---

## 1.6.0 (2024-07-29)

### New

- Warlock MVC. Products. Add API endpoints.
- Warlock MVC. Products. Add image upload functionality.
- Warlock MVC. Products. Implement DataTable for Index.
- Warlock MVC. Products. Implement Sweet Alerts for Delete.

### Change

- Warlock MVC. Products. Refactor Create and Edit to Upsert.

---

## 1.5.0 (2024-07-29)

### New

- Warlock MVC. Containerize the project.

---

## 1.4.0 (2024-07-29)

### New

- Warlock MVC. Implement Products.

---

## 1.3.0 (2024-07-16)

### New

- Warlock MVC. Implement Areas.
- Warlock MVC. Add dropdown menu to navigation.

---

## 1.2.0 (2024-07-16)

### New

- Warlock MVC. Implement repository pattern.

---

## 1.1.0 (2024-07-15)

### New

- Warlock MVC. Implement n-tier architecture.

---

## 1.0.1 (2024-07-15)

### Change

- Warlock MVC. Update UI for Category.

---

## 1.0.0 (2024-07-14)

### New

- Warlock MVC. Base project.
- Warlock MVC Razor. Base razor pages project.

---