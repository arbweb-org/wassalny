var s_map_;
var s_loc_;

// Map script callback
function f_init_map_()
{
    s_map_ = new Microsoft.Maps.Map('#myMap',
        {
            credentials: 'AuZIBmC5pvcoKCjQUsa7WG__SmbOcU9eCJUa1qfjEMfXjBVkmspXebJahDhrp6sm',
            center: new Microsoft.Maps.Location(15.62916511, 32.56757639),
            setLang: 'ar-SA',
            mapTypeId: Microsoft.Maps.MapTypeId.canvasLight,
            showMapTypeSelector: false,
            showLocateMeButton: false,
            showZoomButtons: false,
            zoom: 12
        });

    //Add your post map load code here.
    var center = s_map_.getCenter();

    setInterval(f_get_location_, 1000);
}

function get_center_()
{
    var l_loc_ = s_map_.getCenter();

    //alert(l_loc_.latitude);
    //alert(l_loc_.longitude);
}

function f_center_() { }

function f_send_text_(p_str_)
{
    var l_dta_ = encodeURIComponent(p_str_);
    v_send_text_(l_dta_);
}

function f_get_location_()
{
    v_get_location_('');
}

function v_show_loc_(p_msg_)
{
    $("#b_msg_").text(p_msg_);
}