create database VUZ
go

use VUZ

CREATE TABLE FACULTY
       (FacPK     INTEGER      NOT NULL CONSTRAINT fac_prk PRIMARY KEY,
        Name      VARCHAR(30)  NOT NULL CONSTRAINT fac_unq_nam UNIQUE ,
        Dean      VARCHAR(20)  CONSTRAINT fac_unq_den UNIQUE,
        Building  CHAR(5),
        Fund      NUMERIC(7, 2) CONSTRAINT fac_chk_fnd CHECK (Fund > 0))
go

CREATE TABLE DEPARTMENT
       (DepPK     INTEGER     NOT NULL CONSTRAINT dep_prk PRIMARY KEY,
        FacFK     INTEGER      NOT NULL CONSTRAINT dep_frk_fac REFERENCES FACULTY(FacPK) ON DELETE CASCADE ,
        Name      VARCHAR(30)  NOT NULL,
        Head      VARCHAR(20),
        Building  CHAR(5),
        Fund      NUMERIC(7, 2) CONSTRAINT dep_chk_fnd CHECK (Fund > 0),
        CONSTRAINT dep_unq_nam_frk UNIQUE (Name, FacFK))
go
		
create table POST(id int not null primary key, name varchar(20) NOT NULL UNIQUE, 
constraint tch_chk_pst CHECK(name IN ('профессор', 'доцент', 'преподаватель', 'ассистент')))
go

CREATE TABLE TEACHER
       (TchPK     INTEGER     NOT NULL CONSTRAINT tch_prk PRIMARY KEY,
        DepFK     INTEGER      CONSTRAINT tch_frk_dep REFERENCES DEPARTMENT(DepPK),
        Name      VARCHAR(30)  NOT NULL,
        IDCode    CHAR(10),
        id_Post   integer  not null constraint F_KEY_POST references post(id),
        Tel       CHAR(7),
        Salary    NUMERIC(6, 2) DEFAULT 0 CONSTRAINT tch_chk_sal CHECK (Salary >= 0),
        Rise      NUMERIC(6, 2) DEFAULT 0 CONSTRAINT tch_chk_ris CHECK (Rise >= 0),
        HireDate  DATETIME         CONSTRAINT tch_chk_hdt CHECK (Hiredate >= '01.01.1990'),
        Chief     INTEGER      CONSTRAINT tch_frk_tch REFERENCES TEACHER(TchPK),
	CONSTRAINT UNIQUE_NAME UNIQUE(Name,IDCode),
        CONSTRAINT tch_chk_sal_ris1 CHECK (2 * Rise <= Salary),
        CONSTRAINT tch_chk_sal_ris2 CHECK (Salary + Rise < 4000))
go

CREATE TABLE SGROUP
       (GrpPK     INTEGER  NOT NULL CONSTRAINT grp_prk PRIMARY KEY,
        DepFK     INTEGER   CONSTRAINT grp_frk_dep REFERENCES DEPARTMENT(DepPK),
        Num       NUMERIC(3) NOT NULL,
        Kurs      NUMERIC(1) NOT NULL CONSTRAINT grp_chk_yar CHECK (Kurs IN (1, 2, 3, 4, 5, 6)),
        Quantity  NUMERIC(2) CONSTRAINT grp_chk_qty CHECK (Quantity BETWEEN 1 AND 50),
        Rating    NUMERIC(3) DEFAULT 0 CONSTRAINT grp_chk_rat CHECK (Rating BETWEEN 0 AND 100),
        CurFK     INTEGER   CONSTRAINT grp_frk_tch REFERENCES TEACHER(TchPK),
        CONSTRAINT grp_unq_num_yar_dep UNIQUE (Num, Kurs, DepFK))
go

CREATE TABLE ROOM
       (RomPK     INTEGER   NOT NULL CONSTRAINT rom_prk PRIMARY KEY,
        Num       NUMERIC(3) NOT NULL,
        Floor     NUMERIC(2) CONSTRAINT rom_chk_flr CHECK (Floor BETWEEN 1 AND 16),
        Building  CHAR(5)   NOT NULL,
        Seats     NUMERIC(3) CONSTRAINT rom_chk_set CHECK (Seats BETWEEN 1 AND 300),
        CONSTRAINT rom_unq_num_bld UNIQUE (Num, Building))	
go

CREATE TABLE SUBJECT
       (SbjPK     INTEGER   NOT NULL  CONSTRAINT sbj_prk PRIMARY KEY,
        Name      VARCHAR(50) NOT NULL UNIQUE)
go

create table LECTURE_TYPE (id int not null primary key, name varchar(20) not null unique,
		CONSTRAINT lec_chk_typ CHECK (name IN ('лекция', 'лабораторная работа', 'практика', 'семинар')))
go

CREATE TABLE LECTURE
	(lecPK     INTEGER   NOT NULL  CONSTRAINT lec_prk PRIMARY KEY,
       TchFK    INTEGER      CONSTRAINT lec_frk_tch REFERENCES TEACHER(TchPK) ON DELETE SET NULL,
        GrpFK    INTEGER     NOT NULL CONSTRAINT lec_frk_grp REFERENCES SGROUP(GrpPK) ON DELETE CASCADE,
        SbjFK    INTEGER      CONSTRAINT lec_frk_sbj REFERENCES SUBJECT(SbjPK) ON DELETE SET NULL,
        RomFK    INTEGER      CONSTRAINT lec_frk_rom REFERENCES ROOM(RomPK) ON DELETE SET NULL,
        id_type    int NOT NULL CONSTRAINT lec_typ references lecture_type(id),
        Week     NUMERIC(1)   NOT NULL CONSTRAINT lec_chk_wek CHECK (Week = 1 OR Week = 2),
        Day_week      CHAR(3)     NOT NULL CONSTRAINT lec_chk_day CHECK (Day_week IN ('пнд', 'втр', 'срд', 'чтв', 'птн', 'суб', 'вос')),
        Lesson   NUMERIC(1)   CONSTRAINT lec_chk_les CHECK (Lesson BETWEEN 1 AND 8),
        CONSTRAINT lec_unq1 UNIQUE (TchFK, Week, Day_week, Lesson),
        CONSTRAINT lec_unq2 UNIQUE (GrpFK, Week, Day_week, Lesson),
        CONSTRAINT lec_unq3 UNIQUE (RomFK, Week, Day_week, Lesson))	
go	
		
INSERT INTO FACULTY (FacPK, Name, Dean, Building, Fund) 
VALUES (1, 'информатика', 'Сидоров', '6', 25000);
INSERT INTO FACULTY (FacPK, Name, Dean, Building, Fund) 
VALUES (2, 'кибернетика', 'Петров', '5', 27000);
INSERT INTO FACULTY (FacPK, Name, Dean, Building, Fund) 
VALUES (3, 'математика', 'Игнатов', '3', 23000);

INSERT INTO DEPARTMENT(DepPK, FacFK, Name, Head, Building, Fund) 
VALUES (1, 1, 'базы данных', 'Соколов', '6', 26000);
INSERT INTO DEPARTMENT(DepPK, FacFK, Name, Head, Building, Fund) 
VALUES (2, 1, 'программирование', 'Федоров', '6', 12000);
INSERT INTO DEPARTMENT(DepPK, FacFK, Name, Head, Building, Fund) 
VALUES (3, 1, 'интернет', 'Стрельцов', '3', 10000);
INSERT INTO DEPARTMENT(DepPK, FacFK, Name, Head, Building, Fund) 
VALUES (4, 2, 'теория языков', 'Глущенко', '3', 10000);
INSERT INTO DEPARTMENT(DepPK, FacFK, Name, Head, Building, Fund) 
VALUES (5, 2, 'лингвистика', 'Коробов', '3', 14100);
INSERT INTO DEPARTMENT(DepPK, FacFK, Name, Head, Building, Fund) 
VALUES (6, 2, 'базы данных', 'Тараненко', '5', 27000);

insert into post(id,name) values(1,'профессор');
insert into post(id,name) values(2,'доцент');
insert into post(id,name) values(3,'преподаватель');
insert into post(id,name) values(4,'ассистент');

INSERT INTO TEACHER (TchPK, DepFK, Name, IDCode, id_Post, Tel, Salary, Rise, HireDate, Chief)
VALUES (16, 2, 'Сидоров','0123456789', 1, NULL, 1070, 470, '09.01.1992',  NULL);
INSERT INTO TEACHER (TchPK, DepFK, Name, IDCode, id_Post, Tel, Salary, Rise, HireDate, Chief)
VALUES (4, 2, 'Рамишевский','4567890123', 2, '4567890', 830, 370, '09.09.1998', 16);
INSERT INTO TEACHER (TchPK, DepFK, Name, IDCode, id_Post, Tel, Salary, Rise, HireDate, Chief)
VALUES (2, 2, 'Хоренко','1234567890', 3, NULL, 670, 230, '10.10.2001', 4);
INSERT INTO TEACHER (TchPK, DepFK, Name, IDCode, id_Post, Tel, Salary, Rise, HireDate, Chief)
VALUES (1, 2, 'Вибровский','2345678901', 4, NULL, 570, 170, '09.01.2003', 2);
INSERT INTO TEACHER (TchPK, DepFK, Name, IDCode, id_Post, Tel, Salary, Rise, HireDate, Chief)
VALUES (3, 2, 'Воропаев',NULL, 4, NULL, 570, 150, '09.02.2002', 4);
INSERT INTO TEACHER (TchPK, DepFK, Name, IDCode, id_Post, Tel, Salary, Rise, HireDate, Chief)
VALUES (5, 2, 'Кузинцев','5678901234', 3, '4567890', 630, 270, '01.01.1991', 4);
INSERT INTO TEACHER (TchPK, DepFK, Name, IDCode, id_Post, Tel, Salary, Rise, HireDate, Chief)
VALUES (6, 2, 'Завратинский','3456789012', 2, NULL, 770, 341, '02.01.1996', 16);
INSERT INTO TEACHER (TchPK, DepFK, Name, IDCode, id_Post, Tel, Salary, Rise, HireDate, Chief)
VALUES (8, 2, 'Козлутин','6789012345', 4, 1234567, 530, 220, '07.04.2001', 6);
INSERT INTO TEACHER (TchPK, DepFK, Name, IDCode, id_Post, Tel, Salary, Rise, HireDate, Chief)
VALUES (7, 2, 'Лекарь','3456789012', 2, 1234567, 890, 440, '06.03.1991', 16);
INSERT INTO TEACHER (TchPK, DepFK, Name, IDCode, id_Post, Tel, Salary, Rise, HireDate, Chief)
VALUES (9, 2, 'Мартынов','7890123456', 4, 1234567, 530, 220, '10.02.2002', 5);
INSERT INTO TEACHER (TchPK, DepFK, Name, IDCode, id_Post, Tel, Salary, Rise, HireDate, Chief)
VALUES (10, 2, 'Резван',NULL, 1, 1234567, 1100, 470, '11.11.2000', 16);
INSERT INTO TEACHER (TchPK, DepFK, Name, IDCode, id_Post, Tel, Salary, Rise, HireDate, Chief)
VALUES (11, 2, 'Недеведеев',NULL, 3, 1234567, 570, 230, '07.02.1999', 7);
INSERT INTO TEACHER (TchPK, DepFK, Name, IDCode, id_Post, Tel, Salary, Rise, HireDate, Chief)
VALUES (13, NULL, 'Ахромеев',NULL, 1, 2345678, 1050, 500, '05.05.1998', NULL);
INSERT INTO TEACHER (TchPK, DepFK, Name, IDCode, id_Post, Tel, Salary, Rise, HireDate, Chief)
VALUES (14, NULL, 'Наумов',NULL, 2, 2345678, 770, 350, '09.01.1994', NULL);
INSERT INTO TEACHER (TchPK, DepFK, Name, IDCode, id_Post, Tel, Salary, Rise, HireDate, Chief)
VALUES (15, NULL, 'Дараганов',NULL, 3, 3456789, 570, 250, '11.07.1998',NULL);

INSERT INTO SGROUP (GrpPK, DepFK, Num, Kurs, Quantity, Rating, CurFK)
VALUES (1, 2, 505,5, 27, 67, 7);
INSERT INTO SGROUP (GrpPK, DepFK, Num, Kurs, Quantity, Rating, CurFK)
VALUES (2, 2, 504,5, 31, 55, 6);
INSERT INTO SGROUP (GrpPK, DepFK, Num, Kurs, Quantity, Rating, CurFK)
VALUES (3, 1, 307,3, 33, 48, 5);
INSERT INTO SGROUP (GrpPK, DepFK, Num, Kurs, Quantity, Rating, CurFK)
VALUES (4, 1, 408,4, 27, 56, 5);
INSERT INTO SGROUP (GrpPK, DepFK, Num, Kurs, Quantity, Rating, CurFK)
VALUES (5, 3, 201,2, 35, 43, NULL);

INSERT INTO Room (RomPK, Num, Floor, Building, Seats)
VALUES (1, 311, 3, 6, 17);
INSERT INTO Room (RomPK, Num, Floor, Building, Seats)
VALUES (2, 201, 2, 6, 80);
INSERT INTO Room (RomPK, Num, Floor, Building, Seats)
VALUES (3, 201, 2, 5, 60);
INSERT INTO Room (RomPK, Num, Floor, Building, Seats)
VALUES (4, 202, 2, 4, 70);
INSERT INTO Room (RomPK, Num, Floor, Building, Seats)
VALUES (5, 225, 2, 7, 95);
INSERT INTO Room (RomPK, Num, Floor, Building, Seats)
VALUES (6, 104, 1, 6, 25);
INSERT INTO Room (RomPK, Num, Floor, Building, Seats)
VALUES (7, 110, 1, 6, 40);

INSERT INTO Subject (SbjPK, Name)
VALUES (1, 'операционные системы');
INSERT INTO Subject (SbjPK, Name)
VALUES (2, 'проектирование компьютеров');
INSERT INTO Subject (SbjPK, Name)
VALUES (3, 'базы данных');
INSERT INTO Subject (SbjPK, Name)
VALUES (4, 'охрана труда');
INSERT INTO Subject (SbjPK, Name)
VALUES (5, 'теория алгоритмов');
INSERT INTO Subject (SbjPK, Name)
VALUES (6, 'нейрокомпьютеры');
INSERT INTO Subject (SbjPK, Name)
VALUES (7, 'теория права');

insert into lecture_type (id,name) values(1,'лекция');
insert into lecture_type (id,name) values(2,'лабораторная работа');
insert into lecture_type (id,name) values(3,'практика');
insert into lecture_type (id,name) values(4,'семинар');



INSERT INTO LECTURE (lecPK,TchFK, GrpFK, SbjFK, RomFK, id_type, Week, Day_week, Lesson)
VALUES (1,1, 1, 1, 1, 2, 1, 'пнд', 1);
INSERT INTO LECTURE (lecPK,TchFK, GrpFK, SbjFK, RomFK, id_type, Week, Day_week, Lesson)
VALUES (2,2, 1, 1, 2, 1, 1, 'пнд', 2);
INSERT INTO LECTURE (lecPK,TchFK, GrpFK, SbjFK, RomFK, id_type, Week, Day_week, Lesson)
VALUES (3,3, 1, 2, 1, 2, 1, 'втр', 3);
INSERT INTO LECTURE (lecPK,TchFK, GrpFK, SbjFK, RomFK, id_type, Week, Day_week, Lesson)
VALUES (4,4, 1, 2, 2, 1, 1, 'втр', 4);
INSERT INTO LECTURE (lecPK,TchFK, GrpFK, SbjFK, RomFK, id_type, Week, Day_week, Lesson)
VALUES (5,5, 1, 3, 2, 1, 1, 'срд', 3);
INSERT INTO LECTURE (lecPK,TchFK, GrpFK, SbjFK, RomFK, id_type, Week, Day_week, Lesson)
VALUES (6,6, 1, 4, 3, 1, 1, 'срд', 4);
INSERT INTO LECTURE (lecPK,TchFK, GrpFK, SbjFK, RomFK, id_type, Week, Day_week, Lesson)
VALUES (7,7, 1, 5, 4, 1, 1, 'чтв', 2);
INSERT INTO LECTURE (lecPK,TchFK, GrpFK, SbjFK, RomFK, id_type, Week, Day_week, Lesson)
VALUES (8,8, 1, 4, 5, 3, 1, 'чтв', 3);
INSERT INTO LECTURE (lecPK,TchFK, GrpFK, SbjFK, RomFK, id_type, Week, Day_week, Lesson)
VALUES (9,9, 1, 3, 1, 2, 1, 'чтв', 4);
INSERT INTO LECTURE (lecPK,TchFK, GrpFK, SbjFK, RomFK, id_type, Week, Day_week, Lesson)
VALUES (10,10, 1, 6, 2, 1, 1, 'птн', 1);
INSERT INTO LECTURE (lecPK,TchFK, GrpFK, SbjFK, RomFK, id_type, Week, Day_week, Lesson)
VALUES (11,11, 1, 5, 6, 2, 1, 'птн', 4);


INSERT INTO LECTURE (lecPK,TchFK, GrpFK, SbjFK, RomFK, id_type, Week, Day_week, Lesson)
VALUES (12,2, 1, 1, 2, 1, 2, 'пнд', 2);
INSERT INTO LECTURE (lecPK,TchFK, GrpFK, SbjFK, RomFK, id_type, Week, Day_week, Lesson)
VALUES (13,15, 1, 7, 2, 1, 2, 'пнд', 3);
INSERT INTO LECTURE (lecPK,TchFK, GrpFK, SbjFK, RomFK, id_type, Week, Day_week, Lesson)
VALUES (14,11, 1, 5, 1, 2, 2, 'втр', 2);
INSERT INTO LECTURE (lecPK,TchFK, GrpFK, SbjFK, RomFK,id_type, Week, Day_week, Lesson)
VALUES (15,5, 1, 3, 2, 1, 2, 'втр', 3);
INSERT INTO LECTURE (lecPK,TchFK, GrpFK, SbjFK, RomFK, id_type, Week, Day_week, Lesson)
VALUES (16,4, 1, 2, 2, 1, 2, 'втр', 4);
INSERT INTO LECTURE (lecPK,TchFK, GrpFK, SbjFK, RomFK, id_type, Week, Day_week, Lesson)
VALUES (17,1, 1, 1, 6, 2, 2, 'срд', 2);
INSERT INTO LECTURE (lecPK,TchFK, GrpFK, SbjFK, RomFK, id_type, Week, Day_week, Lesson)
VALUES (18,9, 1, 3, 1, 2, 2, 'срд', 3);
INSERT INTO LECTURE (lecPK,TchFK, GrpFK, SbjFK, RomFK, id_type, Week, Day_week, Lesson)
VALUES (19,15, 1, 7, 7, 2, 2, 'чтв', 1);
INSERT INTO LECTURE (lecPK,TchFK, GrpFK, SbjFK, RomFK, id_type, Week, Day_week, Lesson)
VALUES (20,7, 1, 5, 2, 1, 2, 'чтв', 2);
INSERT INTO LECTURE (lecPK,TchFK, GrpFK, SbjFK, RomFK, id_type, Week, Day_week, Lesson)
VALUES (21,10, 1, 6, 2, 1, 2, 'птн', 1);
INSERT INTO LECTURE (lecPK,TchFK, GrpFK, SbjFK, RomFK, id_type, Week, Day_week, Lesson)
VALUES (22,3, 1, 2, 1, 2, 2, 'птн', 3);

	