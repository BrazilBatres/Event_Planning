CREATE DATABASE event_planning;

USE event_planning;

CREATE TABLE Identification_types (
  id INT NOT NULL AUTO_INCREMENT,
  name VARCHAR(50) NOT NULL,
  PRIMARY KEY (id)
);

CREATE TABLE Item_category (
  id INT NOT NULL AUTO_INCREMENT,
  category VARCHAR(50) NOT NULL,
  PRIMARY KEY (id)
);

CREATE TABLE Roles (
  id INT PRIMARY KEY,
  role_name VARCHAR(50)
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
  role_id INT NOT NULL,
  UNIQUE(email),
  PRIMARY KEY (id),
  FOREIGN KEY (role_id) REFERENCES Roles(id)
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

CREATE TABLE Verification_requests (
  id INT PRIMARY KEY AUTO_INCREMENT,
  seller_id INT NOT NULL,
  admin_id INT NULL,
  description VARCHAR(255) NULL,
  admin_comments VARCHAR(255) NULL,
  status_id INT NOT NULL,
  transac_date datetime NOT NULL,
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
  FOREIGN KEY (seller_id) REFERENCES Sellers(id),
  FOREIGN KEY (item_category_id) REFERENCES Item_category(id)
);
