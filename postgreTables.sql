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
