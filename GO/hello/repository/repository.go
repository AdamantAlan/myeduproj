package repository

import (
	"context"
	"fmt"
	"hello/db"
	"hello/user"
)

// Все репозитории тут
type UserRepository interface {
	GetByID(ctx context.Context, id int) (*user.User, error)
}

// Репо юзеров
type PostgresUserRepository struct {
	db *db.Database
}

// Конструктор юзеров
func NewPostgresUserRepository(db *db.Database) *PostgresUserRepository {
	return &PostgresUserRepository{
		db: db,
	}
}

func (r *PostgresUserRepository) GetByID(
	ctx context.Context,
	id int,
) (*user.User, error) {
	fmt.Println("query postgres")

	return &user.User{
		ID:   id,
		Name: "Dmitriy",
	}, nil
}
