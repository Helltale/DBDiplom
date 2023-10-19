--====================================создание таблиц

--пациент
create table Patient(
omc varchar(16) primary key,
diagnosis_enter varchar(500),
snils varchar(16),
full_name varchar(100)
)
select * from Patient
drop table Patient


--доп информация
create table Additional_information(
omc varchar(16) references patient(OMC) primary key,
allergy varchar(500),
rh varchar(1),
blood varchar(1)
)
select * from AdditionalInformation
drop table Additional_information


--отделение
create table Type_department(
id_department varchar(10) primary key,
name_department varchar(500)
)
select * from Type_department
drop table Type_department


--палата
create table Room( 
number_room varchar(4),
code_hir_department varchar(10),
id_department varchar(4),
sit_empt varchar(2),
sit_bisy varchar(2),
primary key(number_room, code_hir_department, id_department)
)
select * from Room
drop table Room


--сотрудники
create table Staff(
id_staff varchar(10) primary key,
full_name varchar(500),
phone varchar(10),
id_department varchar(10),
code_hir_department varchar(10)
)
select * from Staff
drop table Staff


--хирургический стационар
create table Hir_hospital(
code_hir_department varchar(10) primary key,
name_hir_department varchar(100) unique,
adress_hir_department varchar(500) unique,
boss_hir_department varchar(10) references Staff(id_staff),
phone_hir_department varchar(10) unique
)
select * from Hir_hospital
drop table Hir_hospital


--отделение
create table Department(
code_hir_department varchar(10) references Hir_hospital(code_hir_department),
id_department varchar(4) references Type_department(id_department),
boss_department varchar(10),
primary key(code_hir_department, id_department)
)
select * from Department
drop table Department


----установление связей (криво косо, но по дургому не будет подключаться)
alter table Staff
add constraint FK_Staff foreign key(code_hir_department, id_department) references Department(code_hir_department, id_department)

alter table Department
add constraint FK_Staff1 foreign key(boss_department) References Staff(id_staff)

alter table Room
add constraint FK_Room1 foreign key (code_hir_department, id_department) references Department(code_hir_department, id_department)
----


--паспортные данные
create table Passport(
сode_pass varchar(6) primary key,
data_get date,
number_pass varchar(6),
who_give varchar(100),
tally_pass varchar(4),
adress_pass varchar(100),
id_staff varchar(10) references Staff(id_staff),
omc varchar(16) references Patient(omc),
unique(data_get, number_pass, who_give, tally_pass)
)
select * from Passport
drop table Passport


--сотрудники
create table Receptionist(
id_staff varchar(10) primary key references Staff(id_staff)
)
create table Guard_nurse(
id_staff varchar(10) primary key references Staff(id_staff)
)
create table Therapist(
id_staff varchar(10) primary key references Staff(id_staff)
)
select * from Receptionist
drop table Receptionist


--первичный осмотр
create table Initial_inspection(
omc varchar(16) references Patient(omc),
date_initial date,
doc_receptinoist varchar(10) references Receptionist(id_staff),
diagnosis varchar(500),
primary Key(omc, date_initial)
)
select * from Initial_inspection
drop table Initial_inspection


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
)
select * from Patient_in_ward
drop table Patient_in_ward


--пациенты доктора
create table Doc_Patient(
id_patient varchar(10) references Patient_in_room(id_patient),
id_staff varchar(10) references Therapist(id_staff),
primary key(id_patient, id_staff)
)
select * from Doc_Patient
drop table Doc_Patient


--выписной лист
create table Extract_document(
numb_extract varchar(10) primary key,
id_patient varchar(10),
id_staff varchar(10),
date_extract date,
diagnosis_extract varchar(500),
constraint FK_Extract foreign key (id_patient, id_staff)References Doc_Patient(id_patient, id_staff),
unique (id_patient, id_staff, date_extract)
)
select * from Extract_document
drop table Extract_document


--листы о нетрудоспособности
create table List_not_working(
numb_extract varchar(10) references Extract_document(numb_extract) primary key,
date_open_list date,
date_close_list date
)
select * from List_not_working
drop table List_not_working


--оперативное лечение
create table Operation(
id_operation varchar(10),
date_operation date,
id_staff varchar(10),
id_patient varchar(10),
name_operation varchar(500),
discriptionary_operation varchar(1000),
discriptionary_bad varchar(1000),
constraint FK_Operation1 foreign key (id_patient, id_staff) references Doc_Patient(id_patient, id_staff),
primary key(id_operation, date_operation, id_staff, id_patient)
)
select * from Operation
drop table Operation


--кончервативное лечение
Create Table Сonservative(
id_staff varchar(10),
id_patient varchar(10),
id_procedure varchar(10),
id_staff_nurce varchar(10) references Guard_Nurse(id_staff),
preparat varchar(1000),
procedure_name varchar(1000),
numbers_procedure smallint,
constraint FK_Operation foreign key (id_patient, id_staff) references Doc_Patient(id_patient, id_staff),
primary key(id_staff, id_patient)
)
select * from Сonservative
drop table Сonservative

