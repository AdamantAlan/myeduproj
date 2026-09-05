package services

type User struct {
	Name    string
	Age     age
	IsAdmin bool
}

type age struct {
	value byte
}

func (u *User) SetAdmin() bool {
	if u.Age.value >= 18 {
		u.IsAdmin = true
	}

	return u.IsAdmin
}
