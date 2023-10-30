--====================================создание таблиц

--пациент

create table patient(
omc varchar(16) primary key,
diagnosis_enter varchar(500),
snils varchar(16),
full_name varchar(100)
);

--информация о уже имеющемся больничном листе
create table Add_information_not_working_already(
id_not_working_initial varchar(10) primary key,
omc varchar(16) references patient(OMC),
date_not_working date
);

--доп информация
create table Additional_information(
omc varchar(16) references patient(OMC) primary key,
allergy varchar(500),
rh varchar(1),
blood varchar(1),
additional_info varchar(1000),
adress_real varchar(200)
);

--отделение
create table Type_department(
id_department varchar(10) primary key,
name_department varchar(500)
);

--палата
create table Room( 
number_room varchar(4),
code_hir_department varchar(10),
id_department varchar(4),
sit_empt varchar(2),
sit_bisy varchar(2),
primary key(number_room, code_hir_department, id_department)
);

--сотрудники
create table Staff(
id_staff varchar(10) primary key,
full_name varchar(500),
phone varchar(10),
id_department varchar(10),
code_hir_department varchar(10),
mail varchar(100)
);

--информация о пользователе БД
create table User_info(
	id_staff varchar(10) references Staff(id_staff) primary key,
	login_user varchar(50),
	password_user varchar(50),
	role_user varchar(1),--здесь будет буква или цифра, которая будет означать роль сотрудника при работе с бд
	trust_user varchar(1)
);

--хирургический стационар
create table Hir_hospital(
code_hir_department varchar(10) primary key,
name_hir_department varchar(100) unique,
adress_hir_department varchar(500) unique,
boss_hir_department varchar(10) references Staff(id_staff),
phone_hir_department varchar(10) unique,
ogrm_hir_department varchar(13) unique
);

--отделение
create table Department(
code_hir_department varchar(10) references Hir_hospital(code_hir_department),
id_department varchar(4) references Type_department(id_department),
boss_department varchar(10),
primary key(code_hir_department, id_department)
);

----установление связей (криво косо, но по дургому не будет подключаться)
alter table Staff
--drop constraint FK_Staff
add constraint FK_Staff foreign key(code_hir_department, id_department) references Department(code_hir_department, id_department);

alter table Department
--drop constraint FK_Staff1
add constraint FK_Staff1 foreign key(boss_department) References Staff(id_staff);

alter table Room
--drop constraint FK_Room1
add constraint FK_Room1 foreign key (code_hir_department, id_department) references Department(code_hir_department, id_department);

--паспортные данные
create table Passport(
	сode_pass varchar(6) primary key,
	data_get date,
	number_pass varchar(6),
	--series_pass varchar(4),
	who_give varchar(100),
	tally_pass varchar(6),
	adress_pass varchar(200),
	id_staff varchar(10) references Staff(id_staff),
	omc varchar(16) references Patient(omc),
	unique(data_get, number_pass, who_give, tally_pass)
);

--сотрудники
create table Receptionist(
id_staff varchar(10) primary key references Staff(id_staff)
);
create table Guard_nurse(
id_staff varchar(10) primary key references Staff(id_staff)
);
	create table Therapist(
id_staff varchar(10) primary key references Staff(id_staff)
);

--первичный осмотр
create table Initial_inspection(
omc varchar(16) references Patient(omc),
date_initial date,
doc_receptinoist varchar(10) references Receptionist(id_staff),
diagnosis varchar(500),
primary Key(omc, date_initial)
);

--пациенты в палате
create table Patient_in_room(
id_patient varchar(10) primary key,
omc varchar(16),
code_hir_department varchar(10),
date_room date,
number_room varchar(4),
id_department varchar(4),
constraint FK_PiR1 foreign key(number_room, code_hir_department, id_department) references Room(number_room, code_hir_department, id_department),
constraint FK_PiR2 foreign key (omc, date_room) references Initial_inspection(omc, date_initial),
unique (omc, code_hir_department, date_room, number_room, id_department)
);

--пациенты доктора
create table Doc_Patient(
id_patient varchar(10) references Patient_in_room(id_patient),
id_staff varchar(10) references Therapist(id_staff),
primary key(id_patient, id_staff)
);


--выписной лист
create table Extract_document(
numb_extract varchar(10) primary key,
id_patient varchar(10),
id_staff varchar(10),
date_extract date,
diagnosis_extract varchar(500),
recomendations varchar(2000),
death_mark varchar(1),
constraint FK_Extract foreign key (id_patient, id_staff)References Doc_Patient(id_patient, id_staff),
unique (id_patient, id_staff, date_extract)
);

--листы о нетрудоспособности
create table List_not_working(
numb_extract varchar(10) references Extract_document(numb_extract) primary key,
date_in date,
omc varchar(16),
id_not_working_initial varchar(10) references add_information_not_working_already(id_not_working_initial),
constraint FK_LNW1 foreign key (date_in, omc) references initial_inspection(date_initial, omc)
);

--оперативное лечение
create table Operation(
id_operation varchar(10),
date_operation date,
time_operation time,
id_staff varchar(10),
id_patient varchar(10),
name_operation varchar(500),
discriptionary_operation varchar(5000),
discriptionary_bad varchar(5000),
constraint FK_Operation1 foreign key (id_patient, id_staff) references Doc_Patient(id_patient, id_staff),
primary key(id_operation, date_operation, time_operation, id_staff, id_patient)
);

--таблица с препаратами
create table drug(
id_drug varchar(10) primary key,
name_drug varchar(50)
);

--таблица с вариантами процедур
create table procedures_(
id_procedure varchar(10) primary key,
id_drug varchar(10) references drug(id_drug),
name_drocedure varchar(50),
value_drug int,
value_name varchar(25)
);

--кончервативное лечение
Create Table Сonservative(
id_staff varchar(10), --лечащий врач
id_patient varchar(10),
id_procedure varchar(10) references procedures_(id_procedure),
id_staff_nurce varchar(10) references Guard_Nurse(id_staff), --мед сестра, что делала
date_procedure date,
time_procedure time,
constraint FK_Operation foreign key (id_patient, id_staff) references Doc_Patient(id_patient, id_staff),
primary key(id_staff, id_patient)
);

--осмотры лечащим врачом пациента
create table meetings(
id_meeting varchar(10) primary key,
id_staff varchar(10),
id_patient varchar(10),
discription_meeting varchar(5000),
date_meeting date,
time_meeting time, 
operation_control varchar(5000),
constraint FK_M foreign key (id_patient, id_staff) references Doc_Patient(id_patient, id_staff)
);