package db

type Database struct {
	ConnectionString string
}

func NewDatabase() *Database {
	return &Database{
		ConnectionString: "postgres://localhost/test",
	}
}
