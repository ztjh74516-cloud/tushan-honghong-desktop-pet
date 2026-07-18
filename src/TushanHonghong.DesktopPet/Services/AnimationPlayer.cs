namespace TushanHonghong.DesktopPet.Services;

public sealed class AnimationPlayer
{
    private readonly IReadOnlyList<int> _frames;
    private readonly bool _loops;
    private int _frameIndex;

    public AnimationPlayer(IReadOnlyList<int> frames, bool loops)
    {
        ArgumentNullException.ThrowIfNull(frames);

        if (frames.Count == 0)
        {
            throw new ArgumentException("Animation must contain at least one frame.", nameof(frames));
        }

        _frames = frames;
        _loops = loops;
    }

    public int CurrentFrame => _frames[_frameIndex];

    public void Advance()
    {
        if (_frameIndex == _frames.Count - 1)
        {
            _frameIndex = _loops ? 0 : _frameIndex;
            return;
        }

        _frameIndex++;
    }

    public void MoveToFrame(int frame)
    {
        for (var index = 0; index < _frames.Count; index++)
        {
            if (_frames[index] == frame)
            {
                _frameIndex = index;
                return;
            }
        }

        throw new ArgumentOutOfRangeException(nameof(frame), "Frame is not part of this animation.");
    }

    public void Reset() => _frameIndex = 0;
}
