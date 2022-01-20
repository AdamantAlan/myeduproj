$(
    function anima() {
        var anim1 = {
            marginTop: '25px',
        }
        var anim4 = {
            marginTop: '0px',
        }
        var settings = {
            queue: true,
            duration: 3000
        }
        $(".rubin").animate(anim1, settings)
            .animate(anim4, settings);
        anima();
    }
);