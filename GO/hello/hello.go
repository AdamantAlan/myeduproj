package main

import (
	"fmt"

	"github.com/go-playground/validator/v10"
)

type CreateUserRequest struct {
	Name  string `validate:"required,min=2,max=50"`
	Email string `validate:"required,email"`
	Age   int    `validate:"gte=18,lte=100"`
}

func main() {
	validate := validator.New()

	req := CreateUserRequest{
		Name:  "D",
		Email: "wrong",
		Age:   15,
	}

	err := validate.Struct(req)
	if err != nil {
		for _, e := range err.(validator.ValidationErrors) {
			fmt.Println(
				e.Field(),
				e.Tag(),
				e.Param(),
			)
		}
	}
}
