package quick

//Быстрая сортировка
func QuickSort(nums []int, start int, end int) {
	if start >= end {
		return
	}
	var pivotIndex int = (start + end) / 2
	var pivot int = nums[pivotIndex]

	i, j := start, end

	for i <= j {
		for nums[i] < pivot {
			i++
		}

		for nums[j] > pivot {
			j--
		}

		if i <= j {
			nums[i], nums[j] = nums[j], nums[i]

			i++
			j--
		}
	}

	QuickSort(nums, start, j)
	QuickSort(nums, i, end)
}
