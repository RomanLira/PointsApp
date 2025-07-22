let selectedPointGroup = null;
let selectedPointData = null;
let selectedCommentData = null;

$(document).ready(function () {
    const width = 800;
    const height = 600;

    const stage = new Konva.Stage({
        container: 'container',
        width: width,
        height: height
    });

    const layer = new Konva.Layer();
    stage.add(layer);

    function loadPoints() {
        $.ajax({
            url: '/api/points',
            method: 'GET',
            success: function (data) {
                layer.destroyChildren();

                data.forEach(p => {
                    drawPoint(p);
                });

                layer.draw();

                $('#edit-point-form').hide();
                $('#edit-comment-form').hide();
            }
        });
    }

    function drawPoint(point) {
        const group = new Konva.Group({
            x: point.x,
            y: point.y,
            draggable: true,
        });

        const circle = new Konva.Circle({
            radius: point.radius,
            fill: point.color
        });

        circle.on('dblclick', () => {
            $.ajax({
                url: `/api/points/${point.id}`,
                method: 'DELETE',
                success: () => {
                    group.destroy();
                    $('#edit-point-form').hide();
                    $('#edit-comment-form').hide();
                    layer.draw();
                }
            });
        });

        circle.on('click', (e) => {
            $('#edit-comment-form').hide();
            
            selectedPointGroup = group;
            selectedPointData = point;
            
            const absPos = stage.container().getBoundingClientRect();
            const pointerPos = stage.getPointerPosition();

            $('#edit-point-radius').val(point.radius);
            $('#edit-point-color').val(point.color);
            $('#edit-point-form').css({
                left: absPos.left + pointerPos.x + 10,
                top: absPos.top + pointerPos.y + 10,
                display: 'block'
            });
        });

        group.on('dragend', () => {
            $('#edit-point-form').hide();
            $('#edit-comment-form').hide();
            
            const newX = group.x();
            const newY = group.y();

            $.ajax({
                url: `/api/points/${point.id}`,
                method: 'PUT',
                contentType: 'application/json',
                data: JSON.stringify({
                    id: point.id,
                    x: newX,
                    y: newY,
                    radius: point.radius,
                    color: point.color
                })
            });
        });

        const comments = point.comments || [];
        comments.forEach((comment, i) => {
            const fontSize = 12;
            const padding = 4;
            const lineHeight = fontSize + padding * 2;

            const tempText = new Konva.Text({
                text: comment.text,
                fontSize: fontSize,
                padding: padding,
            });

            const textWidth = tempText.width();
            const labelWidth = textWidth + padding * 2;
            
            const text = new Konva.Label({
                x: -labelWidth / 2,
                y: point.radius + i * (lineHeight + 8)
            });

            text.add(new Konva.Tag({
                fill: comment.color,
                width: labelWidth,
                height: lineHeight,
                stroke: 'black',
                strokeWidth: 1
            }));

            text.add(new Konva.Text({
                text: comment.text,
                fontSize: 16,
                padding: padding,
                fill: 'black',
                align: 'center',
                width: labelWidth
            }));

            text.on('click', () => {
                $('#edit-point-form').hide();
                
                selectedCommentData = comment;
                
                const absPos = stage.container().getBoundingClientRect();
                const pointerPos = stage.getPointerPosition();

                $('#edit-comment-text').val(comment.text);
                $('#edit-comment-color').val(comment.color);

                $('#edit-comment-form').css({
                    left: absPos.left + pointerPos.x + 10,
                    top: absPos.top + pointerPos.y + 10,
                    display: 'block'
                });
            });
            
            text.on('dblclick', () => {
                $.ajax({
                    url: `/api/comments/${comment.id}`,
                    method: 'DELETE',
                    success: () => {
                        text.destroy();
                        $('#edit-point-form').hide();
                        $('#edit-comment-form').hide();
                        layer.draw();
                    }
                });
            });
            
            group.add(text);
        });

        group.add(circle);
        layer.add(group);
    }

    $('#add-point').click(function () {
        const radius = parseInt($('#point-radius').val()) || 20;
        const color = $('#point-color').val() || '#000000';
        
        const newPoint = {
            x: Math.floor(Math.random() * 700) + 50,
            y: Math.floor(Math.random() * 500) + 50,
            radius: radius,
            color: color
        };

        $.ajax({
            url: '/api/points',
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(newPoint),
            success: () => loadPoints()
        });
    });

    $('#delete-all-points').click(function () {
        $.ajax({
            url: '/api/points',
            method: 'DELETE',
            contentType: 'application/json',
            success: () => loadPoints()
        });
    });

    $('#add-comment').click(function () {
        const text = $('#comment-text').val().trim();
        const backgroundColor = $('#comment-color').val();

        if (!text) 
            return alert("Комментарий не может быть пустым");

        $.ajax({
            url: `/api/comments`,
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({
                pointId: selectedPointData.id,
                text: text,
                color: backgroundColor
            }),
            success: () => {
                $('#comment-text').val('');
                $('#comment-color').val('#ffffff');
                $('#comment-form').slideUp();
                loadPoints();
            }
        });
    });

    $('#save-point-edit').click(function () {
        const newRadius = parseInt($('#edit-point-radius').val()) || selectedPointData.radius;
        const newColor = $('#edit-point-color').val() || selectedPointData.color;

        $.ajax({
            url: `/api/points/${selectedPointData.id}`,
            method: 'PUT',
            contentType: 'application/json',
            data: JSON.stringify({
                id: selectedPointData.id,
                x: selectedPointGroup.x(),
                y: selectedPointGroup.y(),
                radius: newRadius,
                color: newColor
            }),
            success: () => {
                loadPoints(); 
            }
        });
    });

    $('#cancel-point-edit').click(() => {
        $('#edit-point-form').hide();
    });

    $('#show-comment-form').click(function () {
        $('#comment-form').slideDown();
    });

    $('#cancel-comment').click(function () {
        $('#comment-text').val('');
        $('#comment-color').val('#ffffff');
        $('#comment-form').slideUp();
    });

    $('#save-comment-edit').click(function () {
        const newText = $('#edit-comment-text').val().trim();
        const newColor = $('#edit-comment-color').val();

        if (!newText) 
            return alert('Текст комментария не может быть пустым');

        $.ajax({
            url: `/api/comments/${selectedCommentData.id}`,
            method: 'PUT',
            contentType: 'application/json',
            data: JSON.stringify({
                id: selectedCommentData.id,
                pointId: selectedCommentData.pointId,
                text: newText,
                color: newColor
            }),
            success: () => {
                loadPoints();
            }
        });
    });

    $('#cancel-comment-edit').click(() => {
        $('#edit-comment-form').hide();
    });

    $('#point-radius, #edit-point-radius').on('input', function () {
        this.value = this.value.replace(/[^\d]/g, '');
        
        let val = parseInt(this.value, 10);
        
        if (isNaN(val)) 
            return;
        
        if (val < 1) val = 1;
        if (val > 100) val = 100;

        this.value = val;
    });

    loadPoints();
});