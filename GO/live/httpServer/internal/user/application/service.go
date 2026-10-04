package application

import (
	"clean-gin/internal/user/domain"
	"context"
)

type Service struct {
	repository Repository
}

func NewService(repository Repository) *Service {
	return &Service{
		repository: repository,
	}
}

func (s *Service) Create(
	ctx context.Context,
	user domain.User,
) (domain.User, error) {
	return s.repository.Create(ctx, user)
}

func (s *Service) GetByID(
	ctx context.Context,
	id int64,
) (domain.User, error) {
	return s.repository.GetByID(ctx, id)
}

func (s *Service) GetAll(
	ctx context.Context,
) ([]domain.User, error) {
	return s.repository.GetAll(ctx)
}

func (s *Service) Update(
	ctx context.Context,
	user domain.User,
) (domain.User, error) {
	return s.repository.Update(ctx, user)
}

func (s *Service) Delete(
	ctx context.Context,
	id int64,
) error {
	return s.repository.Delete(ctx, id)
}
