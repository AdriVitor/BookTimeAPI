CREATE TABLE uf (
    id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    acronym CHAR(2) NOT NULL UNIQUE
);

INSERT INTO uf (name, acronym) VALUES
('Acre', 'AC'),
('Alagoas', 'AL'),
('Amapá', 'AP'),
('Amazonas', 'AM'),
('Bahia', 'BA'),
('Ceará', 'CE'),
('Distrito Federal', 'DF'),
('Espírito Santo', 'ES'),
('Goiás', 'GO'),
('Maranhão', 'MA'),
('Mato Grosso', 'MT'),
('Mato Grosso do Sul', 'MS'),
('Minas Gerais', 'MG'),
('Pará', 'PA'),
('Paraíba', 'PB'),
('Paraná', 'PR'),
('Pernambuco', 'PE'),
('Piauí', 'PI'),
('Rio de Janeiro', 'RJ'),
('Rio Grande do Norte', 'RN'),
('Rio Grande do Sul', 'RS'),
('Rondônia', 'RO'),
('Roraima', 'RR'),
('Santa Catarina', 'SC'),
('São Paulo', 'SP'),
('Sergipe', 'SE'),
('Tocantins', 'TO');


create table resource(
	Id SERIAL not null primary key,
	IdUser int not null,
	IdUf int not null,
	Name varchar(100) not null,
	Description varchar(250) not null,
	Address varchar(150) not null,
	Created_at timestamp with time zone,
	
	constraint fk_uf foreign key (IdUf) references Uf(id)
);