CREATE DATABASE VegetableFruit

USE VegetableFruit

CREATE TABLE VegetableFruit
(
	Id INT PRIMARY KEY NOT NULL,
	[Name] NVARCHAR(50) NOT NULL,
	[Type] NVARCHAR(50) NOT NULL,
	Color NVARCHAR(10) NOT NULL,
	Calories INT NOT NULL
);


INSERT INTO VegetableFruit (Id, [Name], [Type], Color, Calories)
VALUES
    (1, 'Apple', 'Fruit', 'Red', 50),
    (2, 'Banana', 'Fruit', 'Yellow', 101),
    (3, 'Orange', 'Fruit', 'Orange', 56),
    (4, 'Carrot', 'Vegetable', 'Orange', 45),
    (5, 'Lettuce', 'Vegetable', 'Green', 5),
    (6, 'Strawberry', 'Fruit', 'Red', 45),
    (7, 'Grapes', 'Fruit', 'Purple', 69),
    (8, 'Spinach', 'Vegetable', 'Green', 21),
    (9, 'Tomato', 'Vegetable', 'Red', 20),
    (10, 'Blueberry', 'Fruit', 'Blue', 46),
    (11, 'Broccoli', 'Vegetable', 'Green', 55),
    (12, 'Pineapple', 'Fruit', 'Yellow', 55),
    (13, 'Cucumber', 'Vegetable', 'Green', 18),
    (14, 'Cherry', 'Fruit', 'Red', 58),
    (15, 'Potato', 'Vegetable', 'Brown', 57),
    (16, 'Mango', 'Fruit', 'Yellow', 6),
    (17, 'Bell Pepper', 'Vegetable', 'Red', 45),
    (18, 'Pear', 'Fruit', 'Green', 52),
    (19, 'Eggplant', 'Vegetable', 'Purple', 52),
    (20, 'Peach', 'Fruit', 'Orange', 95),
    (21, 'Celery', 'Vegetable', 'Green', 6),
    (22, 'Raspberry', 'Fruit', 'Red', 82),
    (23, 'Zucchini', 'Vegetable', 'Green', 17),
    (24, 'Plum', 'Fruit', 'Purple', 76),
    (25, 'Asparagus', 'Vegetable', 'Green', 2),
    (26, 'Kiwi', 'Fruit', 'Brown', 5),
    (27, 'Brussels Sprouts', 'Vegetable', 'Green', 56),
    (28, 'Apricot', 'Fruit', 'Orange', 44),
    (29, 'Radish', 'Vegetable', 'Red', 17),
    (30, 'Blackberry', 'Fruit', 'Black', 50),
    (31, 'Watermelon', 'Fruit', 'Green', 30);
