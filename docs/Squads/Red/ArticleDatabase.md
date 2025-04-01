-- SQL Script for Article Database Schema

-- Table: Author
CREATE TABLE Author (
    author_id INT PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(100) NOT NULL,
    email VARCHAR(150) NOT NULL UNIQUE
);

-- Table: Article
CREATE TABLE Article (
    article_id INT PRIMARY KEY AUTO_INCREMENT,
    title VARCHAR(255) NOT NULL,
    content TEXT NOT NULL,
    published_at DATETIME,
    author_id INT,
    FOREIGN KEY (author_id) REFERENCES Author(author_id)
);