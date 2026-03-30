create TABLE signal (
	primary_key VARCHAR(36) PRIMARY KEY,
	signal_type INTEGER,
	parameters TEXT,
	create_time DATETIME);
create TABLE point_signal(
	primary_key VARCHAR(36) PRIMARY KEY,
	signal_id VARCHAR(36),
	time FLOAT,
	value FLOAT,
	description TEXT,
	FOREIGN key (signal_id) REFERENCES signal(primary_key));
create table history (
	primary_key VARCHAR(36) PRIMARY key,
	signal_id VARCHAR(36),
	generate_time DATETIME,
	description TEXT,
	FOREIGN key (signal_id) REFERENCES signal(primary_key));