function f_flyout_() { v_toggle_flyout_(''); }

var s_map_ =
{
    s_map_: null,       // Bing Map object
    r_drw_: false,      // Is accuracy circle already rendered?
    r_loc_:
    {
        s_lat_: null,   // Latitude
        s_lng_: null,   // Longitude
        s_acu_: null    // Accuracy in meters
    },
    r_crc_: null,       // Accuracy circle
    r_dot_: null,       // Device location pin point (center of the circle)

    get s_loc_() { return this.r_loc_; },

    set s_loc_(p_crd_)
    {
        // If app is still aquiring location
        if (p_crd_ == '') { return; }

        // Set location
        var l_loc_ = p_crd_.split(',');
        this.r_loc_.s_lat_ = l_loc_[0];
        this.r_loc_.s_lng_ = l_loc_[1];
        this.r_loc_.s_acu_ = l_loc_[2];

        // Render the accuracy circle
        if (!this.r_drw_)
        {
            this.r_drw_ = false;
        }

        // Move the accuracy circle
    }
};

// Map script callback
function f_init_map_()
{
    s_map_.s_map_ = new Microsoft.Maps.Map('#b_map_',
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

    Microsoft.Maps.loadModule(
        'Microsoft.Maps.SpatialMath',
        function ()
        {
            setInterval(f_get_location_, 1000);
        });

    //Add your post map load code here.
}

// Centers the map to device location
function f_center_map_()
{
    var l_loc_ = s_map_.s_loc_;
    if (l_loc_.s_acu_ == null) { return; }      // If app is still aquiring location

    s_map_.s_map_.setView({ center: new Microsoft.Maps.Location(l_loc_.s_lat_, l_loc_.s_lng_) });
}

// Call csharp code to get the location
function f_get_location_()
{ v_get_location_(''); }

// The call back that fires after f_get_location_
// p_msg_: string 'lat,lng,acu', '' If app still aquiring location
function f_set_location_(p_msg_)
{ s_map_.s_loc_ = p_msg_; }