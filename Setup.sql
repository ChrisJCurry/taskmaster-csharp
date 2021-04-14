-- TRUNCATE profiles;
-- USE amazencurry;
-- CREATE TABLE todos
-- (
--     id INT NOT NULL AUTO_INCREMENT,
--     title VARCHAR(255) NOT NULL,
--     creatorId VARCHAR(255) NOT NULL,

--     PRIMARY KEY (id)
-- );

DROP TABLE todos;
CREATE TABLE todos
(
    id INT NOT NULL AUTO_INCREMENT,
    title VARCHAR(255) NOT NULL,
    creatorId VARCHAR(255) NOT NULL,
    completed TINYINT NOT NULL,
    PRIMARY KEY (id),

    FOREIGN KEY (creatorId) 
        REFERENCES profiles (id)
        ON DELETE CASCADE
);

CREATE TABLE goals
(
    id INT NOT NULL AUTO_INCREMENT,
    description VARCHAR(255) NOT NULL,
    creatorId VARCHAR(255) NOT NULL,
    todoId INT NOT NULL,
    completed TINYINT NOT NULL,

    PRIMARY KEY (id),

    FOREIGN KEY (creatorId) 
        REFERENCES profiles (id)
        ON DELETE CASCADE,

    FOREIGN KEY (todoId) 
        REFERENCES todos (id)
        ON DELETE CASCADE
)

