package infrastructure

import (
	"clean-gin/internal/user/domain"
	"context"
	"errors"
	"sync"
)

var ErrUserNotFound = errors.New("user not found")

type MemoryRepository struct {
	mu     sync.RWMutex
	users  map[int64]domain.User
	nextID int64
}

func NewMemoryRepository() *MemoryRepository {
	return &MemoryRepository{
		users:  make(map[int64]domain.User),
		nextID: 1,
	}
}

func (r *MemoryRepository) Create(
	ctx context.Context,
	user domain.User,
) (domain.User, error) {
	r.mu.Lock()
	defer r.mu.Unlock()

	user.ID = r.nextID
	r.nextID++

	r.users[user.ID] = user

	return user, nil
}

func (r *MemoryRepository) GetByID(
	ctx context.Context,
	id int64,
) (domain.User, error) {
	r.mu.RLock()
	defer r.mu.RUnlock()

	user, ok := r.users[id]
	if !ok {
		return domain.User{}, ErrUserNotFound
	}

	return user, nil
}

func (r *MemoryRepository) GetAll(
	ctx context.Context,
) ([]domain.User, error) {
	r.mu.RLock()
	defer r.mu.RUnlock()

	result := make([]domain.User, 0, len(r.users))

	for _, user := range r.users {
		result = append(result, user)
	}

	return result, nil
}

func (r *MemoryRepository) Update(
	ctx context.Context,
	user domain.User,
) (domain.User, error) {
	r.mu.Lock()
	defer r.mu.Unlock()

	if _, ok := r.users[user.ID]; !ok {
		return domain.User{}, ErrUserNotFound
	}

	r.users[user.ID] = user

	return user, nil
}

func (r *MemoryRepository) Delete(
	ctx context.Context,
	id int64,
) error {
	r.mu.Lock()
	defer r.mu.Unlock()

	if _, ok := r.users[id]; !ok {
		return ErrUserNotFound
	}

	delete(r.users, id)

	return nil
}
