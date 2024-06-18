<body class="bg-gray-100 min-h-screen flex">
    <!-- Contenido existente -->
    <script>
        new Vue({
            el: '#app',
        data: {
            activeRestaurant: null,
        restaurants: @Html.Raw(Newtonsoft.Json.JsonConvert.SerializeObject(ViewBag.Restaurantes)),
        dishes: [
        {
            id: 1,
        name: 'Margherita Pizza',
        restaurantId: 1,
        image: 'https://example.com/margherita.jpg',
        prepTime: '20 - 25 min',
        rating: 4.8,
        description: 'A delicious Margherita Pizza',
        price: 10.99
                    },
        {
            id: 2,
        name: 'California Roll',
        restaurantId: 2,
        image: 'https://example.com/california-roll.jpg',
        prepTime: '15 - 20 min',
        rating: 4.7,
        description: 'Fresh California Roll',
        price: 8.99
                    },
        {
            id: 3,
        name: 'Cheeseburger',
        restaurantId: 3,
        image: 'https://example.com/cheeseburger.jpg',
        prepTime: '10 - 15 min',
        rating: 4.9,
        description: 'Juicy Cheeseburger',
        price: 9.99
                    }
        ]
            },
        methods: {
            selectRestaurant(restaurant) {
            this.activeRestaurant = restaurant.Id;
                }
            }
        });
    </script>
</body>
