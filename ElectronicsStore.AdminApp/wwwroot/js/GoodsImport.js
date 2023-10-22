var GoodsImport = function () {
    this.initialize = function () {
        loadCart();
        loadListCart();
        loadListCartBill();
        regsiterEvents();
    }
    function loadCart() {
        $.ajax({
            type: "GET",
            url: '/Import/LoadCart',
            success: function (res) {
                var count = 0;
                $.each(res, function (i, item) {
                    count += 1;
                });
                $('#number-items').text(count);
            }
        });
    }

    function loadListCart() {
        $.get('/Import/LoadCart').done(function (response, statusText, xhr) {
            if (xhr.status === 200) {
                var template = $('#tmpl_load_cart').html();
                if (response) {
                    var html = '';
                    var count = 0;
                    $.each(response, function (index, item) {
                        html += Mustache.render(template, {
                            image: $('#_configuration').val() + '/Image/' + item.image,
                            id: item.id,
                            name: item.name,
                            quanlity: item.quanlity
                        });
                        count += 1;
                    });
                    $('#list_items').html(html);
                    $('#number-items').text(count);
                }
            }
        });
    }

    function loadListCartBill() {
        $.get('/Import/LoadCart').done(function (response, statusText, xhr) {
            if (xhr.status === 200) {
                var template = $('#tmpl_load_cart_bill').html();
                if (response) {
                    var html = '';
                    var count = 0;
                    $.each(response, function (index, item) {
                        html += Mustache.render(template, {
                            image: $('#_configuration').val() + '/Image/' + item.image,
                            id: item.id,
                            name: item.name,
                            quanlity: item.quanlity,
                            price: item.price
                        });
                        count += 1;
                    });
                    $('#list_items_bill').html(html);
                    $('#number-items').text(count);
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
                url: '/Import/AddToCart',
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

        $('body').on('change', '.ipt-quan', function (e) {
            e.preventDefault();
            const id = $(this).data('id');
            const quantity = parseInt($('#txt_quantity_' + id).val());
            updateQuanlityCart(id, quantity);
        });
        $('body').on('change', '.ipt-price', function (e) {
            e.preventDefault();
            const id = $(this).data('id');
            const price = parseInt($('#txt_price_' + id).val());
            updatePriceCart(id, price);
        });
        $('body').on('click', '.btn-remove', function (e) {
            e.preventDefault();
            const id = $(this).data('id');
            updateQuanlityCart(id, 0);
        });
        $('#btn_add_attachment').on('click', function () {
            $('#attachment_items').append('<p><input type="file" name="image" /></p>');
            return false;
        });
    }

    function updateQuanlityCart(id, quan) {
        $.ajax({
            type: "POST",
            url: '/Import/UpdateQuanlityCart',
            data: {
                productId: id,
                quanlity: quan
            },
            success: function (res) {
                loadListCart();
                loadListCartBill();
            },
            error: function (err) {
                console.log(err);
            }
        });
    }

    function updatePriceCart(id, price) {
        $.ajax({
            type: "POST",
            url: '/Import/UpdatePriceCart',
            data: {
                productId: id,
                importPrice: price
            },
            success: function (res) {
            },
            error: function (err) {
                console.log(err);
            }
        });
    }
}


/*
$('body').on('click', '.btn-plus', function (e) {
    e.preventDefault();
    const id = $(this).data('id');
    const quantity = parseInt($('#txt_quantity_' + id).val()) + 1;
    updateCart(id, quantity);
});
$('body').on('click', '.btn-minus', function (e) {
    e.preventDefault();
    const id = $(this).data('id');
    const quantity = parseInt($('#txt_quantity_' + id).val()) - 1;
    updateCart(id, quantity);
});
$('body').on('click', '.btn-confirm', function (e) {
    e.preventDefault();
    const id = $(this).data('id');
    const quantity = parseInt($('#txt_quantity_' + id).val());
    const price = parseInt($('#txt_price_' + id).val());
    UpdateCartBill(id, quantity, price);
});

function UpdateCartBill(id, quan, price) {
    $.ajax({
        type: "POST",
        url: '/Import/UpdateCartBill',
        data: {
            productId: id,
            quanlity: quan,
            importPrice: price
        },
        success: function (res) {
            loadListCart();
        },
        error: function (err) {
            console.log(err);
        }
    });
}
*/