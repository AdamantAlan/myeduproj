
// WaitGroup
func main() {
	var wg sync.WaitGroup

	wg.Add(2)

	go func() {
		defer wg.Done()
		fmt.Println("goroutine 1")
	}()

	go func() {
		defer wg.Done()
		fmt.Println("goroutine 2")
	}()

	wg.Go(func() {
		fmt.Println("worker 1")
	})

	wg.Wait()

	fmt.Println("Все goroutine завершились")
}

// SELECT
func main() {
	ch := make(chan string)

	go func() {
		time.Sleep(5 * time.Second)
		ch <- "hello"
	}()

	select {
	case msg := <-ch:
		fmt.Println(msg)

	case <-time.After(2 * time.Second):
		fmt.Println("timeout")
	}
}

// WaitGroup
func main() {
	var wg sync.WaitGroup

	wg.Add(2)
	funcc := func(i int) {
		defer wg.Done()
		fmt.Println("goroutine ", i)
	}
	go funcc(1)

	go funcc(2)

	wg.Go(func() {
		fmt.Println("goroutine ", 3)
	})

	wg.Wait()

	fmt.Println("Все goroutine завершились")
}

//CONTEXT
func worker(ctx context.Context) {
	for {
		select {
		case <-ctx.Done():
			fmt.Println(ctx.Err())
			return

		default:
			fmt.Println("работаю...")
			time.Sleep(time.Second)
		}

		if ctx.Err() != nil {
			return
		}
	}
}

func main() {
	ctx, cancel := context.WithCancel(context.Background())
	//ctx, cancel := context.WithTimeout(context.Background(), 3*time.Second)
	//ctx, cancel := context.WithDeadline(context.Background(), time.Now().Add(5*time.Second))
	go worker(ctx)

	time.Sleep(500 * time.Millisecond)

	cancel()

	time.Sleep(time.Second)
}

//MUTEX
func main() {
	var mu sync.Mutex
	counter := 0

	var wg sync.WaitGroup

	for i := 0; i < 10000; i++ {
		wg.Add(1)

		go func() {
			defer wg.Done()
			defer mu.Unlock()

			mu.Lock()
			counter++
		}()
	}

	wg.Wait()

	fmt.Println(counter)
}

//ATOMIC
// var counter atomic.Int64
// var stopped atomic.Bool
// var config atomic.Pointer[Config]
// Load()
// Store(value)
// Add(delta)
// Swap(value)
// CompareAndSwap(old, new)

//ERRGROUP
func main() {
	g, ctx := errgroup.WithContext(context.Background())

	g.Go(func() error {
		select {
		case <-ctx.Done():
			return ctx.Err()
		case <-time.After(2 * time.Second):
			fmt.Println("task 1 done")
			return nil
		}
	})

	g.Go(func() error {
		time.Sleep(500 * time.Millisecond)
		return fmt.Errorf("task 2 failed")
	})

	g.Go(func() error {
		select {
		case <-ctx.Done():
			fmt.Println("task 3 cancelled")
			return ctx.Err()
		case <-time.After(5 * time.Second):
			return nil
		}
	})

	if err := g.Wait(); err != nil {
		fmt.Println("group error:", err)
	}
}

//SEMAPFORE
func main() {
	ctx := context.Background()

	sem := semaphore.NewWeighted(3)

	var wg sync.WaitGroup

	for i := 1; i <= 10; i++ {
		wg.Add(1)

		go func(id int) {
			defer wg.Done()

			// занять 1 слот
			if err := sem.Acquire(ctx, 1); err != nil {
				fmt.Println("Acquire error:", err)
				return
			}

			defer sem.Release(1)

			fmt.Println("start:", id)

			time.Sleep(time.Second)

			fmt.Println("finish:", id)
		}(i)
	}

	wg.Wait()
}