/*CREATE DATABASE event_planning;*/

USE event_planning;
DROP TABLE IF exists Event_Products;
DROP TABLE IF exists Event_Services;
DROP TABLE IF exists Products;
DROP TABLE IF exists Services;
DROP TABLE IF exists Events;
DROP TABLE IF exists Referrals;
DROP TABLE IF exists Verification_requests;
DROP TABLE IF exists Verification_status;
DROP TABLE IF exists Seller_social_media;
DROP TABLE IF exists Sellers;
DROP TABLE IF exists Buyers;
DROP TABLE IF exists Administrators;
DROP TABLE IF exists user_roles;
DROP TABLE IF exists Users;
DROP TABLE IF exists User_type;
DROP TABLE IF exists Service_category;
DROP TABLE IF exists Product_category;
DROP TABLE IF exists Identification_types;
DROP TABLE IF exists Cities;
DROP TABLE IF exists States;
DROP TABLE IF exists Countries;
DROP TABLE IF exists role_permissions;
DROP TABLE IF exists Permissions;
DROP TABLE IF exists Roles;

CREATE TABLE Identification_types (
  id INT NOT NULL AUTO_INCREMENT,
  name VARCHAR(50) NOT NULL,
  PRIMARY KEY (id)
);

CREATE TABLE Service_category (
  id INT NOT NULL AUTO_INCREMENT,
  category VARCHAR(50) NOT NULL,
  PRIMARY KEY (id)
);

CREATE TABLE Product_category (
  id INT NOT NULL AUTO_INCREMENT,
  category VARCHAR(50) NOT NULL,
  PRIMARY KEY (id)
);

CREATE TABLE Countries (
  id INT NOT NULL AUTO_INCREMENT,
  name VARCHAR(50) NOT NULL,
  PRIMARY KEY (id)
);

CREATE TABLE States (
  id INT NOT NULL AUTO_INCREMENT,
  name VARCHAR(50) NOT NULL,
  country_id INT NOT NULL,
  PRIMARY KEY (id),
  FOREIGN KEY (country_id) REFERENCES Countries(id)
);

CREATE TABLE Cities (
  id INT NOT NULL AUTO_INCREMENT,
  name VARCHAR(50) NOT NULL,
  state_id INT NOT NULL,
  PRIMARY KEY (id),
  FOREIGN KEY (state_id) REFERENCES States(id)
);

CREATE TABLE Users (
  id INT NOT NULL AUTO_INCREMENT,
  first_name VARCHAR(50) NOT NULL,
  last_name VARCHAR(50) NOT NULL,
  company_name VARCHAR(50) NULL,
  mail_visible TINYINT NOT NULL DEFAULT 0,
  email VARCHAR(100) NOT NULL,
  phone_visible TINYINT NOT NULL DEFAULT 0,
  contact_phone VARCHAR(20) NULL,
  is_company TINYINT NOT NULL DEFAULT 0,
  password VARCHAR(100) NOT NULL,
  UNIQUE(email),
  PRIMARY KEY (id)
);

CREATE TABLE Sellers (
  id INT NOT NULL AUTO_INCREMENT,
  user_id INT NOT NULL,
  company_name VARCHAR(100) NULL,
  identification_type_id INT NOT NULL,
  identification_number VARCHAR(100) NOT NULL,
  experience_years INT,
  freelance TINYINT NOT NULL DEFAULT 0,
  PRIMARY KEY (id),
  FOREIGN KEY (user_id) REFERENCES Users(id),
  FOREIGN KEY (identification_type_id) REFERENCES identification_types(id)
);

CREATE TABLE Verification_status (
  id INT PRIMARY KEY,
  name VARCHAR(50) NOT NULL
);

CREATE TABLE Referrals (
  id INT NOT NULL AUTO_INCREMENT,
  seller_id INT NOT NULL,
  name VARCHAR(50) NOT NULL,
  phone VARCHAR(20) NOT NULL,
  email VARCHAR(100) NOT NULL,
  PRIMARY KEY (id),
  FOREIGN KEY (seller_id) REFERENCES sellers(id)
);

CREATE TABLE Seller_social_media (
    id INT NOT NULL AUTO_INCREMENT,
    seller_id INT NOT NULL,
    social_media_name VARCHAR(50) NOT NULL,
    social_media_url VARCHAR(255) NOT NULL,
    PRIMARY KEY (id),
    FOREIGN KEY (seller_id) REFERENCES sellers(id)
);

CREATE TABLE Roles (
  id INT PRIMARY KEY,
  role_name VARCHAR(50)
);

CREATE TABLE Permissions (
  id INT PRIMARY KEY,
  permission_name VARCHAR(50)
);

CREATE TABLE user_roles (
  id INT PRIMARY KEY,
  user_id INT,
  role_id INT,
  start_date DATE,
  end_date DATE,
  FOREIGN KEY (user_id) REFERENCES Users(id),
  FOREIGN KEY (role_id) REFERENCES Roles(id)
);


CREATE TABLE role_permissions (
  id INT PRIMARY KEY,
  role_id INT,
  permission_id INT,
  FOREIGN KEY (role_id) REFERENCES Roles(id),
  FOREIGN KEY (permission_id) REFERENCES Permissions(id)
);

CREATE TABLE Verification_requests (
  id INT PRIMARY KEY AUTO_INCREMENT,
  seller_id INT NOT NULL,
  admin_id INT NULL,
  description VARCHAR(255) NULL,
  admin_comments VARCHAR(255) NULL,
  status_id INT NOT NULL,
  transac_date DATE NOT NULL,
  FOREIGN KEY (seller_id) REFERENCES Sellers(id),
  FOREIGN KEY (admin_id) REFERENCES Users(id),
  FOREIGN KEY (status_id) REFERENCES Verification_status(id)
);

CREATE TABLE Catalog_Item (
  id INT NOT NULL AUTO_INCREMENT,
  seller_id INT NOT NULL,
  item_name VARCHAR(100) NOT NULL,
  item_description VARCHAR(1000) NOT NULL,
  item_price DECIMAL(10,2) NOT NULL,
  item_category_id INT NOT NULL,
  is_service TINYINT NOT NULL DEFAULT 0,
  PRIMARY KEY (id),
  FOREIGN KEY (seller_id) REFERENCES Users(id),
  FOREIGN KEY (product_category_id) REFERENCES Product_category(id)
);


CREATE TABLE Events (
  id INT NOT NULL AUTO_INCREMENT,
  buyer_id INT NOT NULL,
  event_name VARCHAR(100) NOT NULL,
  event_date DATE NOT NULL,
  event_location VARCHAR(100) NOT NULL,
  event_description VARCHAR(1000) NOT NULL,
  event_budget DECIMAL(10,2) NOT NULL,
  PRIMARY KEY (id),
  FOREIGN KEY (buyer_id) REFERENCES Users(id)
);

CREATE TABLE Event_Items (
  id INT NOT NULL AUTO_INCREMENT,
  event_id INT NOT NULL,
  item_id INT NOT NULL,
  quantity INT NOT NULL,
  PRIMARY KEY (id),
  UNIQUE(event_id, item_id),
  FOREIGN KEY (event_id) REFERENCES Events(id),
  FOREIGN KEY (item_id) REFERENCES Catalog_Item(id)
);

