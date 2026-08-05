CREATE TABLE RESERVATION(
	id SERIAL PRIMARY KEY,
	id_resource INT NOT NULL,
	id_customer int not null,
	startDate TIMESTAMP without TIME ZONE NOT NULL,
	endDate TIMESTAMP without TIME zone not null,
	observation VARCHAR(255) null,
	status INT not null,
	js_config varchar(255)
)