$.getScript("js/chess.js",
    function () {
        var madeMove = false;
        


        var status = "";

        var board,
            boardEl = $("#board");
        game = new Chess(),
            squareClass = "square-55d63",
            moveHistory = 0,
            statusEl = $("#status"),
            squareToHighlight = 0,
            colorToHighlight = 0,
            fenEl = $("#fen"),
            gStateEl = $("#gameState");
            pgnEl = $("#pgn");


        game.load_pgn(pgnEl.val());


// do not pick up pieces if the game is over
// only pick up pieces for the side to move
        var onDragStart = function (source, piece, position, orientation) {
            var notMyMove = false;
            if ((game.turn() === "w" && $("#orientation").html() === "black") ||
                (game.turn() === "b" && $("#orientation").html() === "white") ||
                ($("#gameState").val()!=="STILL_PLAYING"))
                
            {
                notMyMove = true;
            }

            if (game.game_over() === true || madeMove === true || notMyMove === true ||
                (game.turn() === "w" && piece.search(/^b/) !== -1) ||
                (game.turn() === "b" && piece.search(/^w/) !== -1)) {
                

                return false;
            }
        };

        var onDrop = function(source, target) {
            // see if the move is legal
            var move = game.move({
                from: source,
                to: target,
                promotion: "q" // NOTE: always promote to a queen for example simplicity
                  
            });

            if (game.in_checkmate()) {
                if (game.turn() === "w") {
                    gStateEl.val("BLACK_WINS");
                } else {
                    gStateEl.val("WHITE_WINS");
                }
            }
            if (game.in_draw()) {
                gStateEl.val("DRAW");
            }


            // illegal move
            if (move === null) return "snapback";

            updateStatus();
        };

// update the board position after the piece snap 
// for castling, en passant, pawn promotion
        var onSnapEnd = function() {
            board.position(game.fen());
            madeMove = true;
            $("#submit-button").show();
            $("#cancel-button").show();
        };

        var updateStatus = function() {
            status = "";

            var moveColor = "White";
            if (game.turn() === "b") {
                moveColor = "Black";
            }

            // checkmate?
            if (game.in_checkmate() === true) {
                status = "Game over, " + moveColor + " is in checkmate.";
            }
            // draw?
            else if (game.in_draw() === true) {
                status = "Game over, drawn position";
            }
            // game still on
            else {
                status = moveColor + " to move";

                // check?
                if (game.in_check() === true) {
                    status += ", " + moveColor + " is in check";
                }
            }


            statusEl.html(status);
            fenEl.val(game.fen());
            pgnEl.val(game.pgn());

            
        };

        var cfg = {
            draggable: true,
            position: fenEl.val(),
            onDragStart: onDragStart,
            onDrop: onDrop,
            onSnapEnd: onSnapEnd,
            orientation: $("#orientation").html()
    };

        board = ChessBoard("board", cfg);
        var getArrLength = fenEl.val().split(' ').length;
        if (parseInt(fenEl.val().split(' ')[getArrLength - 1]) > 1) {
            $("#resign").show();
            $("#Abort").hide();
        } else {
            $("#resign").hide();
            $("#Abort").show();
        }

        $("#cancel-button").hide();
        $("#submit-button").hide();
        



        var cancelMove = function() {
            game.undo();
            board.position(game.fen());
            updateStatus();
            
            statusEl.html(status);
            fenEl.val(game.fen());
            pgnEl.val(game.pgn());
            $("#cancel-button").hide();
            $("#submit-button").hide();
            madeMove = false;

        };

        $("#cancel-button").on("click", cancelMove);

});