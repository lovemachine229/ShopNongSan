$(function() {

	$("#not div").hover(function(){
	    $(this).animate({ width: "200px" });
	}, function() {
	    $(this).animate({ width: "100px" });
	});

	$("#default div").hover(function(){
	    $(this).stop().animate({ width: "200px" });
	}, function() {
	    $(this).stop().animate({ width: "100px" });
	});

	$("#endfalse div").hover(function(){
	    $(this).stop(true, false).animate({ width: "200px" });
	}, function() {
	    $(this).stop(true, false).animate({ width: "100px" });
	});

	$("#endtrue div").hover(function(){
	    $(this).stop(true, true).animate({ width: "200px" });
	}, function() {
	    $(this).stop(true, true).animate({ width: "100px" });
	});

	$("#no-queue div").hover(function(){
	    $(this).animate({ width: "200px" }, {queue: false});
	}, function() {
	    $(this).animate({ width: "100px" }, {queue: false});
	});
    
	var inAnimation = new Array();

	$("#callback div").hover(function(){
	    if (!inAnimation[$("#callback div").index(this)] ) {
	        $(this).animate({ width: "200px" });
	    }
	}, function() {
	    inAnimation[$("#callback div").index(this)] = true;
	    $(this).animate({ width: "100px" }, "normal", "linear", function() {
	        inAnimation[$("#callback div").index(this)] = false; 
	    })
	});

	$("#animate-test div").hover(function(){
	    $(this).filter(':not(:animated)').animate({ width: "200px" });
	}, function() {
	    $(this).animate({ width: "100px" });
	});

	$("#dequeue div").hover(function(){
		if (!$(this).hasClass('animated')) {
			$(this).dequeue().stop().animate({ width: "200px" });
		}
	}, function() {
	    $(this).addClass('animated').animate({ width: "100px" }, "normal", "linear", function() {
			$(this).removeClass('animated').dequeue();
		});
	});

});