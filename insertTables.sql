INSERT INTO patient (omc, diagnosis_enter, snils, full_name) VALUES 
('1234567890123456', 'Diabetes mellitus', '123-45-6789-01', 'John Smith'),
('2345678901234567', 'Hypertension', '234-56-7890-12', 'Jane Doe'),
('3456789012345678', 'Lung cancer', '345-67-8901-23', 'Bob Johnson'),
('4567890123456789', 'Cancer', '456-78-9012-34', 'Alice Lee'),
('5678901234567890', 'AIDS', '567-89-0123-45', 'Tom Wilson'),
('6789012345678901', 'Alzheimers disease', '678-90-1234-56', 'Sara Lee'),
('7890123456789012', 'Diabetes mellitus', '789-01-2345-67', 'David Smith'),
('8901234567890123', 'Hypertension', '890-12-3456-78', 'Emily Doe'),
('9012345678901234', 'Lung cancer', '901-23-4567-89', 'Karen Johnson');

INSERT INTO Add_information_not_working_already (id_not_working_initial, omc, date_not_working) VALUES 
('1', '1234567890123456', '2021-01-01'),
('2', '2345678901234567', '2021-02-01'),
('3', '3456789012345678', '2021-03-01'),
('4', '4567890123456789', '2021-04-01'),
('5', '5678901234567890', '2021-05-01'),
('6', '6789012345678901', '2021-06-01'),
('7', '7890123456789012', '2021-07-01'),
('8', '8901234567890123', '2021-08-01'),
('9', '9012345678901234', '2021-09-01');

INSERT INTO Additional_information (omc, allergy, rh, blood, additional_info, adress_real) VALUES
('1234567890123456', 'Milk', 'A', 'O', 'None', '123 Main St'),
('2345678901234567', 'Eggs', 'B', 'O', 'None', '456 Elm St'),
('3456789012345678', 'Peanuts', 'A', 'O', 'None', '789 Oak St'),
('4567890123456789', 'Shellfish', 'B', 'O', 'None', '321 Maple Ave'),
('5678901234567890', 'Tree Nuts', 'A', 'O', 'None', '654 Pine St'),
('6789012345678901', 'Wheat', 'B', 'O', 'None', '987 Cedar St'),
('7890123456789012', 'Soy', 'A', 'O', 'None', '246 Birch Rd'),
('8901234567890123', 'Fish', 'B', 'O', 'None', '567 Maple Ave'),
('9012345678901234', 'Milk', 'A', 'O', 'None', '890 Oak St');

INSERT INTO Type_department (id_department, name_department) VALUES 
('1', 'Cardiology'),
('2', 'Oncology'),
('3', 'Neurology'),
('4', 'Urology'),
('5', 'Pediatrics'),
('6', 'Gastroenterology'),
('7', 'Endocrinology'),
('8', 'Dermatology'),
('9', 'Ophthalmology');