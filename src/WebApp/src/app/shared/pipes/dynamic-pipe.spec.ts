import { PipeType } from './dynamic-pipe';

describe('DynamicPipe', () => {
  it('create an instance', () => {
    const pipe = new PipeType();
    expect(pipe).toBeTruthy();
  });
});
