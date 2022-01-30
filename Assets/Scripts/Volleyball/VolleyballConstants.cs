public static class VolleyballConstants
{
    //Game settings

    public const int maxBounces = 3;
    //Camera settings

    public const float cameraSize = 15.0f;

    //The following variables don't do anything, but in the future if we want to add multiple players/balls, we can do that
    public const int startingBallNum = 1;
    public const int playerNum = 2;

    //Ball and player starting constants

    public const float playerStartDispX = 8.0f;
    public const float playerStartY = -8.0f;
    public const float ballStartY = -5.0f;

    //Player movement & physics constants
    public const float playerGravityScale = 10.0f;
    public const float playerMaxJumpTime = 0.375f;
    public const float playerSpeed = 10.0f;
    public const float playerJumpSpeed = 15.0f;
    public const float playerVelocityMultiplier = 1.0f;

    //Ball interaction & physics constants
    public const float ballGravityScale = 1.0f;
    public const float ballBounceMultiplier = 0.5f;
    public const float ballPlayerVelocityAdditionMultiplier = 0.5f;
    public const float playerMaxStoredEnergy = 15.0f;
    public const float playerChargeTime = 1.0f;
    public const float playerChargeSpeedMultiplier = 1.0f;
    public const float ballMaxHitDistance = 1.5f;

}
