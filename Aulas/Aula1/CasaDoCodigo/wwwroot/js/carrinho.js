class Carrinho {
    //Aumentar quantidade pelo botão +
    clickIncremento(btn) {
        let data = this.getData(btn);
        data.Quantidade++;
        this.postQuantidade(data);
    }

    //Diminuir quantidade pelo botão -
    clickDecremento(btn) {
        let data = this.getData(btn);
        data.Quantidade--;
        if (data.Quantidade <= 0) {
            data.Quantidade = 1;
            alert('Quantidade não pode ser menor ou igual a zero')
        }

        this.postQuantidade(data);
    }

    //Alterar quantidade manualmente
    updateQuantidade(input) {
        let data = this.getData(input);
        this.postQuantidade(data);
    }


    //Remove item do carrinho
    removeItempedido(btn) {
        let data = this.getData(btn);
        var linhaDoItem = $(btn).parents('[item-id]');
        $.ajax({
            url: '/pedido/removeitempedido',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data)
        }).done(function (response) {
            debugger;
            linhaDoItem.remove();
            /*******Atualiza dados totais do carrinho***********/
            let carrinho = response.carrinhoViewModel;
            $('[numero-itens]').html('Total: ' + carrinho.itens.length + ' itens');
            $('[total]').html((carrinho.total).duasCasas());
        });
    }

    //Busca os dados dad linha do html
    getData(elemento) {
        var linhaDoItem = $(elemento).parents('[item-id]');
        var itemId = $(linhaDoItem).attr('item-id');
        var novaQtde = $(linhaDoItem).find('input').val();

        return {
            Id: itemId,
            Quantidade: novaQtde
        };
    }

    //Chama o metodo de atualização de quantidade via Ajax
    postQuantidade(data) {
        debugger;
        let token = $('[name=__RequestVerificationToken]').val();

        let headers = {};
        headers['RequestVerificationToken'] = token;
        $.ajax({
            url: '/pedido/updatequantidade',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data),
            headers: headers

        }).done(function (response) {
            debugger;
            /******Atualiza dados da linha do item *******/
            let itemPedido = response.itemPedido;
            let linhaDoItem = $('[item-id=' + itemPedido.id + ']');
            linhaDoItem.find('input').val(itemPedido.quantidade);
            linhaDoItem.find('[subtotal]').html((itemPedido.subtotal).duasCasas());

            /*******Atualiza dados totais do carrinho***********/
            let carrinho = response.carrinhoViewModel;
            $('[numero-itens]').html('Total: ' + carrinho.itens.length + ' itens');
            $('[total]').html((carrinho.total).duasCasas());

        });
    }

}

var carrinho = new Carrinho();

Number.prototype.duasCasas = function () {
    return this.toFixed(2).replace('.', ',');
}



