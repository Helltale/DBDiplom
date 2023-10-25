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

INSERT INTO Hir_hospital (code_hir_department, name_hir_department, adress_hir_department, boss_hir_department, phone_hir_department, ogrm_hir_department)
VALUES 
('1001', 'Медицинская организация №1', 'ул. Ленина, д. 10', 			null, '9373131233', '1234567890123'),
('1002', 'Научно-педагогическая организация №2', 'ул. Пушкина, д. 20',  null, '9374949593', '9876543210987'),
('1003', 'Городская больница №3', 'ул. Ленина, д. 30', 					null, '9372342433', '3210987654123'),
('1004', 'Медицинская организация №4', 'ул. Пушкина, д. 40', 			null, '9375657679', '9876543210123'),
('1005', 'Научно-педагогическая организация №5', 'ул. Ленина, д. 50', 	null, '9377867644', '3454445322378'),
('1006', 'Городская больница №6', 'ул. Пушкина, д. 60', 				null, '9375656878', '3210987654987'),
('1007', 'Медицинская организация №7', 'ул. Ленина, д. 70', 			null, '9373345454', '3424233333433'),
('1008', 'Научно-педагогическая организация №8', 'ул. Пушкина, д. 80', 	null, '9372343111', '1234567890987'),
('1009', 'Городская больница №9', 'ул. Ленина, д. 90', 					null, '9375667778', '6786645653345');

INSERT INTO Department (code_hir_department, id_department, boss_department)
VALUES 
('1001', '1', null),
('1001', '2', null),
('1002', '2', null),
('1002', '3', null),
('1003', '3', null),
('1003', '4', null),
('1004', '4', null),
('1004', '5', null),
('1005', '5', null),
('1005', '6', null),
('1006', '6', null),
('1006', '7', null),
('1007', '7', null),
('1007', '8', null),
('1008', '8', null),
('1008', '9', null),
('1009', '9', null),
('1009', '1', null);

INSERT INTO Room (number_room, code_hir_department, id_department, sit_empt, sit_bisy) VALUES 
('101', '1001', '1', '5', '2'),
('102', '1002', '2', '5', '2'),
('103', '1003', '3', '5', '1'),
('104', '1004', '4', '5', '3'),
('105', '1005', '5', '5', '4'),
('106', '1006', '6', '5', '5'),
('107', '1007', '7', '5', '5'),
('108', '1008', '8', '5', '4'),
('109', '1009', '9', '5', '3');

INSERT INTO Staff (id_staff, full_name, phone, id_department, code_hir_department, mail) VALUES 
('1234567890', 'John Smith1', 	'9376223112', '1', '1001', 'john.smith@example.com'),
('2345678901', 'Jane Doe1', 	'9373453566', '2', '1002', 'jane.doe@example.com'),
('3456789012', 'Bob Johnson1', 	'9371102334', '3', '1003', 'bob.johnson@example.com'),
('4567890123', 'Alice Lee1', 	'9370012382', '4', '1004', 'alice.lee@example.com'),
('5678901234', 'Tom Wilson1', 	'9374568945', '5', '1005', 'tom.wilson@example.com'),
('6789012345', 'Sara Lee1', 	'9373453522', '6', '1006', 'ara.lee@example.com'),
('7890123456', 'David Smith1', 	'9371111232', '7', '1007', 'david.smith@example.com'),
('8901234567', 'Emily Doe1', 	'9377869696', '8', '1008', 'emily.doe@example.com'),
('9012345678', 'Karen Johnson1','9372347179', '9', '1009', 'karen.johnson@example.com');

INSERT INTO User_info (id_staff, login_user, password_user, role_user, trust_user) VALUES 
('1234567890', 'john.smith', '123', '1', 'y'),
('2345678901', 'jane.doe', 'password456', '2', 'y'),
('3456789012', 'bob.johnson', 'password789', '3', 'y'),
('4567890123', 'alice.lee', 'password012', '4', 'y'),
('5678901234', 'tom.wilson', 'password345', '5', 'y'),
('6789012345', 'ara.lee', 'password678', '6', 'y'),
('7890123456', 'david.smith', 'password901', '7', 'y'),
('8901234567', 'emily.doe', 'password234', '8', 'y'),
('9012345678', 'karen.johnson', 'password567', '9', 'n');

Update Hir_hospital set boss_hir_department = '1234567890' where code_hir_department = '1001';
Update Hir_hospital set boss_hir_department = '2345678901' where code_hir_department = '1002';
Update Hir_hospital set boss_hir_department = '3456789012' where code_hir_department = '1003';
Update Hir_hospital set boss_hir_department = '4567890123' where code_hir_department = '1004';
Update Hir_hospital set boss_hir_department = '5678901234' where code_hir_department = '1005';
Update Hir_hospital set boss_hir_department = '6789012345' where code_hir_department = '1006';
Update Hir_hospital set boss_hir_department = '7890123456' where code_hir_department = '1007';
Update Hir_hospital set boss_hir_department = '8901234567' where code_hir_department = '1008';
Update Hir_hospital set boss_hir_department = '9012345678' where code_hir_department = '1009';

--добавить персонал для начальников отделений (во всех отделениях один большой босс)
Update Department set Boss_department = '9012345678' where Code_hir_department = '1001' and ID_department = '1';
Update Department set Boss_department = '9012345678' where Code_hir_department = '1001' and ID_department = '2';
Update Department set Boss_department = '9012345678' where Code_hir_department = '1002' and ID_department = '2';
Update Department set Boss_department = '9012345678' where Code_hir_department = '1002' and ID_department = '3';
Update Department set Boss_department = '9012345678' where Code_hir_department = '1003' and ID_department = '3';
Update Department set Boss_department = '9012345678' where Code_hir_department = '1003' and ID_department = '4';
Update Department set Boss_department = '9012345678' where Code_hir_department = '1004' and ID_department = '4';
Update Department set Boss_department = '9012345678' where Code_hir_department = '1004' and ID_department = '5';
Update Department set Boss_department = '9012345678' where Code_hir_department = '1005' and ID_department = '5';
Update Department set Boss_department = '9012345678' where Code_hir_department = '1005' and ID_department = '6';
Update Department set Boss_department = '9012345678' where Code_hir_department = '1006' and ID_department = '6';
Update Department set Boss_department = '9012345678' where Code_hir_department = '1006' and ID_department = '7';
Update Department set Boss_department = '9012345678' where Code_hir_department = '1007' and ID_department = '7';
Update Department set Boss_department = '9012345678' where Code_hir_department = '1007' and ID_department = '8';
Update Department set Boss_department = '9012345678' where Code_hir_department = '1008' and ID_department = '8';
Update Department set Boss_department = '9012345678' where Code_hir_department = '1008' and ID_department = '9';
Update Department set Boss_department = '9012345678' where Code_hir_department = '1009' and ID_department = '9';
Update Department set Boss_department = '9012345678' where Code_hir_department = '1009' and ID_department = '1';

INSERT INTO passport (сode_pass, data_get, number_pass, who_give, tally_pass, adress_pass, id_staff, omc)
VALUES 
('123456', '2021-01-01', '789012', 'МВД1', '123456', 'ул. Ленина, д. 10', 	null, 	'1234567890123456'),
('234567', '2021-02-01', '890123', 'МВД2', '234567', 'ул. Пушкина, д. 20', 	null, 	'2345678901234567'),
('345678', '2021-03-01', '901234', 'МВД3', '345678', 'ул. Ленина, д. 30', 	null, 	'3456789012345678'),
('456789', '2021-04-01', '012345', 'МВД4', '456789', 'ул. Пушкина, д. 40', 	null, 	'4567890123456789'),
('567890', '2021-05-01', '123456', 'МВД5', '567890', 'ул. Ленина, д. 50', 	null, 	'5678901234567890'),
('678901', '2021-06-01', '234567', 'МВД6', '678901', 'ул. Пушкина, д. 60', 	null, 	'6789012345678901'),
('789012', '2021-07-01', '345678', 'МВД7', '789012', 'ул. Ленина, д. 70', 	null, 	'7890123456789012'),
('890123', '2021-08-01', '456789', 'МВД8', '890123', 'ул. Пушкина, д. 80', 	null, 	'8901234567890123'),
('901234', '2021-09-01', '567890', 'МВД9', '901234', 'ул. Ленина, д. 90', 	null, 	'9012345678901234'),

('123232', '2021-01-01', '445456', 'МВД1', '445456', 'ул. Ленина, д. 11', 	'1234567890', 	null),
('112321', '2021-02-01', '534534', 'МВД2', '534534', 'ул. Пушкина, д. 21', 	'2345678901', 	null),
('675675', '2021-03-01', '445393', 'МВД3', '445393', 'ул. Ленина, д. 31', 	'3456789012', 	null),
('096845', '2021-04-01', '456456', 'МВД4', '456456', 'ул. Пушкина, д. 41', 	'4567890123', 	null),
('567537', '2021-05-01', '212236', 'МВД5', '212236', 'ул. Ленина, д. 51', 	'5678901234', 	null),
('897957', '2021-06-01', '780796', 'МВД6', '780796', 'ул. Пушкина, д. 61', 	'6789012345', 	null),
('444575', '2021-07-01', '345344', 'МВД7', '345344', 'ул. Ленина, д. 71', 	'7890123456', 	null),
('896954', '2021-08-01', '234806', 'МВД8', '234806', 'ул. Пушкина, д. 81', 	'8901234567', 	null),
('564633', '2021-09-01', '564899', 'МВД9', '564899', 'ул. Ленина, д. 91', 	'9012345678', 	null);

Insert into receptionist Values
('1234567890'),('2345678901'),('3456789012');
Insert into receptionist Values
('6789012345'),('4567890123'),('5678901234');
Insert into receptionist Values
('7890123456'),('8901234567'),('9012345678');
























