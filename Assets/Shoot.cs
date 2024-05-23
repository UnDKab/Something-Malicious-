using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject arrowPrefab; // Ссылка на префаб стрелы
    private float timer; // Таймер для отслеживания перезарядки
    public float reloadTime = 1.0f; // Время перезарядки в секундах
    private Animator bowAnimator; // Ссылка на компонент Animator

    void Start()
    {
        // Скрываем курсор мыши
        Cursor.visible = false;

        // Получаем компонент Animator
        bowAnimator = GetComponent<Animator>();
        if (bowAnimator == null)
        {
            Debug.LogError("Animator component not found!");
        }
    }

    void Update()
    {
        // Уменьшаем таймер на количество времени, прошедшее с последнего кадра
        timer -= Time.deltaTime;

        // Проверяем, нажата ли кнопка мыши и истек ли таймер перезарядки
        if (Input.GetMouseButton(0) && timer <= 0)
        {
            // Воспроизводим анимацию атаки
            if (bowAnimator != null)
            {
                bowAnimator.SetTrigger("Attack");
            }

            // Создаем экземпляр стрелы из префаба на позиции и с ротацией текущего объекта
            GameObject arrowInstance = Instantiate(arrowPrefab, transform.position, transform.rotation);

            // Добавляем силу к Rigidbody стрелы, чтобы запустить ее вперед
            Rigidbody arrowRigidbody = arrowInstance.GetComponent<Rigidbody>();
            if (arrowRigidbody != null)
            {
                arrowRigidbody.AddForce(transform.forward * 1000f);
            }

            // Сброс таймера перезарядки
            timer = reloadTime;
        }
    }
}
