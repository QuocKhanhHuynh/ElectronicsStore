var Cart = function () {

    this.initialize = function () {
        loadCart();
        loadListCart();
        regsiterEvents();
    }

    function loadCart() {
        $.ajax({
            type: "GET",
            url: '/Order/LoadCart',
            success: function (res) {
                var count = 0;
                var total = 0;
                $.each(res, function (i, item) {
                    count += item.quanlity;
                    total += item.quanlity * item.price;
                });
                $('#number-items').text(count);
                $('#number-total').text('' + total.toLocaleString('vi-VN') + 'đ');
            }
        });
    }

    function loadListCart() {
        $.get('/Order/LoadCart').done(function (response, statusText, xhr) {
            if (xhr.status === 200) {
                var template = $('#tmpl_load_cart').html();
                if (response) {
                    var html = '';
                    var totalCart = 0;
                    $.each(response, function (index, item) {
                        html += Mustache.render(template, {
                            image: $('#_configuration').val() + '/Image/' + item.image,
                            id: item.id,
                            name: item.name,
                            quanlity: item.quanlity,
                            price: item.price.toLocaleString('vi-VN') + 'đ',
                            total: '' + (item.price * item.quanlity).toLocaleString('vi-VN') + 'đ'
                        });
                        totalCart += item.price * item.quanlity;
                    });
                    $('#list_cart').html(html);
                    $('#number_total').text('' + totalCart.toLocaleString('vi-VN') + 'đ');
                    if (totalCart == 0) {
                        $('#checkout_cart').hide();
                        $('#check_count_cart').html('<h4>Không có sản phẩm trong giỏ hàng</h4>');
                    }
                }
            }
        });
    }

    function regsiterEvents() {
        $('body').on('click', '.btn-add-cart', function (e) {
            e.preventDefault();
            const id = $(this).data('id');
            $.ajax({
                type: "POST",
                url: '/Order/AddToCart',
                data: {
                    productId: id
                },
                success: function (res) {
                    loadCart();
                },
                error: function (err) {
                    console.log(err);
                }
            });
        });

        $('body').on('click', '.btn-plus', function (e) {
            e.preventDefault();
            const id = $(this).data('id');
            const quantity = parseInt($('#txt_quantity_' + id).val()) + 1;
            updateQuanlityCart(id, quantity);
        });
        $('body').on('click', '.btn-minus', function (e) {
            e.preventDefault();
            const id = $(this).data('id');
            const quantity = parseInt($('#txt_quantity_' + id).val()) - 1;
            updateQuanlityCart(id, quantity);
        });
        $('body').on('click', '.btn-remove', function (e) {
            e.preventDefault();
            const id = $(this).data('id');
            updateQuanlityCart(id, 0);
        });
        $('body').on('change', '.ipt-quan', function (e) {
            e.preventDefault();
            const id = $(this).data('id');
            const quantity = parseInt($('#txt_quanlity_' + id).val());
            updateQuanlityCart(id, quantity);
        });
    }

    function updateQuanlityCart(id, quan) {
        $.ajax({
            type: "POST",
            url: '/Order/UpdateQuanlityCart',
            data: {
                productId: id,
                quanlity: quan
            },
            success: function (res) {
                loadListCart();
                loadCart();
            },
            error: function (err) {
                console.log(err);
            }
        });
    }
}