package services

type User struct {
	Name    string
	Age     byte
	IsAdmin bool
}

func (u *User) SetAdmin() bool {
	u.IsAdmin = true

	return u.IsAdmin
}
