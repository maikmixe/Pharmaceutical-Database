CREATE TABLE Samples (
    sample_id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(100),
    quantity INT,
    expirationDate DATE,
    expired BOOLEAN DEFAULT FALSE
);
