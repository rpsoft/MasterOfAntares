using UnityEngine;

public class keyboardControl : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;
    public float pulseSpeed = 1f;
    public float pulseAmount = 0.1f;
    public Color selectionColor = Color.red;
    public ParticleSystem engineEffect;

    private Vector3 originalScale;
    private bool isSelected = false;
    private LineRenderer lineRenderer;
    private Vector3 targetPosition;
    private bool isRotating = false;
    private bool isMoving = false;

    void Start()
    {
        originalScale = transform.localScale;
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 5;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.loop = true;
        lineRenderer.useWorldSpace = true;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.sortingOrder = 10; // Ensure the line renderer is rendered above the sprite
        lineRenderer.enabled = false;
        targetPosition = transform.position;

        // Initialize the engine effect
        if (engineEffect == null)
        {
            engineEffect = gameObject.AddComponent<ParticleSystem>();
        }

        var main = engineEffect.main;
        main.startColor = Color.yellow;
        main.startSize = 0.2f;
        main.startSpeed = 1f;
        main.simulationSpace = ParticleSystemSimulationSpace.World;

        var emission = engineEffect.emission;
        emission.rateOverTime = 0f;

        var shape = engineEffect.shape;
        shape.shapeType = ParticleSystemShapeType.Cone;
        shape.angle = 25f;
        shape.radius = 0.1f;
        shape.position = new Vector3(0, -0.5f, 0); // Adjust position to be at the back of the spaceship
    }

    void Update()
    {
        float moveDirection = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        float rotationDirection = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;

        if (moveDirection != 0 || rotationDirection != 0)
        {
            transform.Translate(0, moveDirection, 0);
            transform.Rotate(0, 0, -rotationDirection);
            transform.localScale = originalScale; // Reset scale when moving

            // Update engine effect intensity
            var emission = engineEffect.emission;
            emission.rateOverTime = Mathf.Abs(moveDirection) * 50f; // Adjust the multiplier as needed
            if (!engineEffect.isPlaying)
            {
                engineEffect.Play();
            }
        }
        else
        {
            // Apply pulsing effect
            float pulse = Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;
            transform.localScale = originalScale + new Vector3(pulse, pulse, pulse);

            // Reduce engine effect intensity
            var emission = engineEffect.emission;
            emission.rateOverTime = 0f;
            if (engineEffect.isPlaying)
            {
                engineEffect.Stop();
            }
        }

        // Check for mouse click
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0; // Ensure the z position is zero for 2D
            Collider2D collider = GetComponent<Collider2D>();
            if (collider != null)
            {
                if (collider.OverlapPoint(mousePos))
                {
                    isSelected = !isSelected;
                    lineRenderer.enabled = isSelected;
                    if (isSelected)
                    {
                        Debug.Log("Selected " + gameObject.name);
                        DrawSelectionSquare();
                    }
                }
                else
                {
                    isSelected = false;
                    lineRenderer.enabled = false;
                }
            }
            else
            {
                isSelected = false;
                lineRenderer.enabled = false;
            }

            // Set target position and start rotating
            targetPosition = mousePos;
            isRotating = true;
            isMoving = false;
        }

        // Rotate towards the target position if rotating
        if (isRotating)
        {
            Vector3 direction = targetPosition - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
            {
                isRotating = false;
                isMoving = true;
            }
        }

        // Move towards the target position if moving
        if (isMoving)
        {
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

            if (Vector3.Distance(transform.position, targetPosition) < 0.001f)
            {
                isMoving = false;
            }
        }

        // Update the selection square position if selected
        if (isSelected)
        {
            DrawSelectionSquare();
        }
    }

    void DrawSelectionSquare()
    {
        Bounds bounds = GetComponent<Renderer>().bounds;
        Vector3[] corners = new Vector3[5];
        corners[0] = new Vector3(bounds.min.x, bounds.min.y, bounds.center.z);
        corners[1] = new Vector3(bounds.min.x, bounds.max.y, bounds.center.z);
        corners[2] = new Vector3(bounds.max.x, bounds.max.y, bounds.center.z);
        corners[3] = new Vector3(bounds.max.x, bounds.min.y, bounds.center.z);
        corners[4] = corners[0]; // Close the loop

        lineRenderer.startColor = selectionColor;
        lineRenderer.endColor = selectionColor;
        lineRenderer.SetPositions(corners);
    }
}