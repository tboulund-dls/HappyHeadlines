export const createSubscription = async (
    email: string,
    subscriptionType: string
): Promise<void> => {
    try {
        const response = await fetch("/api/v1/subscribe/", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify({
                email: email,
                subscriptionType: subscriptionType,
            }),
        });

        if (!response.ok) {
            throw new Error(`Failed to create subscription: ${response.statusText}`);
        }

        console.log(`Subscription for ${subscriptionType} created successfully!`);
        alert(`Subscription for ${subscriptionType} created successfully!`);
    } catch (error) {
        console.error(`Error creating subscription for ${subscriptionType}:`, error);
        alert(`Failed to create subscription for ${subscriptionType}.`);
    }
};