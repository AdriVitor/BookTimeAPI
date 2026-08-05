CREATE TABLE users (
    Id SERIAL primary key,
    name varchar(100) not null,
    email varchar(200) not null,
    password varchar(30) not null,
    cpf varchar(11) not null,
    dateofbirth timestamp with time zone,
    created timestamp with time zone
);

CREATE TABLE roles (
    Id SERIAL primary key,
    Name varchar(35) NOT NULL
);
insert into roles(name) VALUES('Owner');
insert into roles(name) VALUES('Customer');


CREATE TABLE role_user (
    UserId int NOT NULL,
    RoleId int NOT NULL,
    PRIMARY KEY (UserId, RoleId),
    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE,
    FOREIGN KEY (RoleId) REFERENCES Roles(Id) ON DELETE CASCADE
);