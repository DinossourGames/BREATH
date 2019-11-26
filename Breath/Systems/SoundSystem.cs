using System.Collections.Generic;
using Breath.Entities;
using Breath.Utils;
using DinoOtter;

namespace Breath.Systems
{
    public class SoundSystem
    {
        private Game _game;

        private Dictionary<string,Music> _musics;
        private Dictionary<string,Sound> _sounds;
        private Music _current;
        private Sound _currentSound;

        public SoundSystem(Game game)
        {
            _game = game;
            _musics = new Dictionary<string, Music>();
            _sounds = new Dictionary<string, Sound>();
            
            AddMusic("main", new Music(BasePath.Sound("main.wav")));
            //Play("main");
        }

        public bool AddMusic(string track, Music music) => _musics.TryAdd(track,music);
        public bool RemoveMusic(string track) => _musics.Remove(track);
        
        public bool AddSound(string track, Sound sound) => _sounds.TryAdd(track,sound);
        public bool RemoveSound(string track) => _sounds.Remove(track);

        public void Play(string track)
        {
            if(!_musics.ContainsKey(track))
                return;
            
            if(_musics[track].IsPlaying)
                _musics[track].Pause();
            else
                _musics[track].Play();
            _current = _musics[track];
        }

        public void Play()
        {
            if(_current.IsPlaying)
                _current.Pause();
            else
                _current.Play();
        }
        
        public void Stop()
        {
            foreach (var item in _musics) item.Value.Stop();
        }     
        public void StopAll()
        {
            foreach (var item in _musics) item.Value.Stop();
            foreach (var item in _sounds) item.Value.Stop();
        }

        public void PlaySound(string sound)
        {
            if(_sounds[sound].IsPlaying)
                _sounds[sound].Pause();
            else
                _sounds[sound].Play();
        }

        public void PlaySound()
        {
            if(_currentSound.IsPlaying)
                _currentSound.Pause();
            else
                _currentSound.Play();
        }
        public void Volume(float musicValue,float soundValue)
        {
            Music.GlobalVolume = musicValue;
            Sound.GlobalVolume = soundValue;
        }
    }
}